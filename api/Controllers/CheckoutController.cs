using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Checkout;
using api.Interface;
using api.Mapper;
using api.Models;
using Ebooking.Data;
using Ebooking.Interface;
using Ebooking.Migrations;
using Ebooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using stockapi.Extensions;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly IEventRepository EventRepository;
        private readonly IBookingRepository BookingRepository;

        private readonly IEventTicketRepository eventTicketRepository;

        private readonly ICouponCodeRepository couponCodeRepository;

        private readonly ICheckoutRepository CheckoutRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IPaymentRepository PaymentRepository;

        private readonly ApplicationDbContext DbContext;
        public CheckoutController(IEventRepository eventRepository, IBookingRepository bookingRepository, ApplicationDbContext dbContext, IEventTicketRepository eventTicket, ICouponCodeRepository couponCode, ICheckoutRepository checkout, UserManager<ApplicationUser> userManager, IPaymentRepository paymentRepository)
        {
            EventRepository = eventRepository;
            BookingRepository = bookingRepository;
            DbContext = dbContext;
            eventTicketRepository = eventTicket;
            couponCodeRepository = couponCode;
            CheckoutRepository = checkout;
            _userManager = userManager;
            PaymentRepository = paymentRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCheckOutSession([FromBody] CreateCheckoutSessionDTO createCheckoutSessionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /* 
                FLOW
            */
            //get the user from the claims
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            //get the event details
            var EventData = await EventRepository.GetEventById(createCheckoutSessionDTO.EventId);
            if (EventData == null)
            {
                return NotFound("Event Not Found");
            }

            //coupon code validation
            if (!string.IsNullOrWhiteSpace(createCheckoutSessionDTO.CouponCode))
            {
                var CouponData = await couponCodeRepository.GetCouponCodeById(createCheckoutSessionDTO.CouponCode, createCheckoutSessionDTO.EventId);
                if (CouponData == null) return BadRequest("Coupon Invalid for this Event");
                if (CouponData.Expiry < DateTime.Now) return BadRequest("Coupon Expired");
                if (CouponData.CurrentUsage <= 0) return BadRequest("Coupon Expired");
                if (CouponData.CurrentUsage < createCheckoutSessionDTO.TotalTickets) return BadRequest("Coupon Expired");
                createCheckoutSessionDTO.DiscountAmount = (CouponData.DiscountPercentage / 100) * createCheckoutSessionDTO.TotalPrice;
                createCheckoutSessionDTO.FinalAmount = createCheckoutSessionDTO.TotalPrice - createCheckoutSessionDTO.DiscountAmount.Value;
            }
            else
            {
                createCheckoutSessionDTO.FinalAmount = createCheckoutSessionDTO.TotalPrice;
            }

            //validate booking count doesn't exceed the limit of booking per account
            var BookingCount = await BookingRepository.GetBookingsCount(createCheckoutSessionDTO.EventId, user.Id);
            if (BookingCount + createCheckoutSessionDTO.TotalTickets > EventData.MaxTicketsPerAccount)
            {
                return BadRequest($"You can only book {EventData.MaxTicketsPerAccount} tickets for this event from this account");
            }


            //2. validate tickets with the actual event and ticket information
            foreach (var ticket in createCheckoutSessionDTO.Tickets)
            {
                var ticketInfo = await eventTicketRepository.GetTicketTypebyID(ticket.TicketId);

                //ticket not found check
                if (ticketInfo == null) return BadRequest($"{ticket.TicketDisplayName} Not Found");


                //invalid ticket id for the event check
                if (ticketInfo.Event.Id != createCheckoutSessionDTO.EventId) return BadRequest("Event doesn't have the provided ticketType");

                //more than available tickets check
                if (ticket.TicketQuantity > ticketInfo.AvailableTickets)
                {
                    return BadRequest($"Sorry {ticket.TicketDisplayName} is  not available now");
                }

                //Invalid pricing check
                if (ticket.SingleticketPrice != ticketInfo.TicketPrice)
                {
                    return BadRequest($"Invalid pricing for {ticket.TicketDisplayName}");
                }
            }

            // create a checkout session
            var CheckoutStatus = await CheckoutRepository.CreateCheckoutSession(createCheckoutSessionDTO.CreateCheckoutFromDTO(user.Id));
            if (CheckoutStatus.Id.Equals(Guid.Empty) || CheckoutStatus == null)
            {
                return BadRequest("Failed to create a checkout session");
            }

            //create a payment session
            var PaymentStatus = await PaymentRepository.CreatePaymentInformation(CheckoutStatus.CreatePaymentSession());
            if (PaymentStatus.Id.Equals(Guid.Empty) || PaymentStatus == null)
            {
                return BadRequest("Failed to create a payment session");
            }
            /*
            Send Back the response :{
                CheckoutSessionId,PaymentSessionID
            }
             */
            return Ok(new
            {
                message = "Checkout Session Created Successfully. It's Valid for next 30 minutes",
                CheckoutID = CheckoutStatus.Id,
                PaymentID = PaymentStatus.Id
            });
        }

        [HttpGet]
        [Route("/CouponCode")]
        public async Task<IActionResult> ValidateCouponCodeAndReturn([FromQuery] string Code, [FromQuery] Guid EventID)
        {
            //get the event details
            var EventData = await EventRepository.GetEventById(EventID);
            if (EventData == null)
            {
                return NotFound("Event Not Found");
            }

            var CouponData = await couponCodeRepository.GetCouponCodeById(Code, EventID);

            if (CouponData == null) return NotFound("Coupon Invalid for this Event");

            return Ok(CouponData.CreateDTOForCoupon());
        }

        [HttpPost]
        [Route("/CouponCode/Add")]
        public async Task<IActionResult> PostAnEventCoupon([FromBody] CreateCouponDTO createCouponDTO)
        {
            //get the event details
            var EventData = await EventRepository.GetEventById(createCouponDTO.EventId);
            if (EventData == null)
            {
                return NotFound("Event Not Found");
            }
            var Coupon = createCouponDTO.CreateCouponFromDTO();
            await couponCodeRepository.CreateCouponForEvent(Coupon);
            return Ok("Coupon created successfully");
        }

        [Authorize]
        [HttpPost]
        [Route("Booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreatePaymentDTO createPaymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get the user from the claims
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            //get the event details
            var EventData = await EventRepository.GetEventById(createPaymentDTO.EventID);
            if (EventData == null)
            {
                return NotFound("Event Not Found");
            }

            //AdditionalCheck: Check if the tickets are available
            if (createPaymentDTO.TicketsBought > EventData.AvailableTickets)
            {
                return BadRequest("Tickets not available now");
            }

            //get checkout session
            var CheckoutData = await CheckoutRepository.GetCheckoutSession(createPaymentDTO.CheckoutID);
            if (CheckoutData == null || CheckoutData.IsCheckoutSessionExpired || CheckoutData.Status != Models.CheckoutStatus.Pending)
            {
                return BadRequest("Checkout Session not found or is Expired");
            }
            //TODO: || CheckoutData.AppUserID != user.Id this validation after altering database

            //get payment session
            var PaymentData = await PaymentRepository.GetPaymentInformation(createPaymentDTO.PaymentID);
            if (PaymentData == null)
            {
                return BadRequest("Payment Session not found or is Expired");
            }
            if (PaymentData.IsPaymentSessionExpired)
            {
                return BadRequest("Payment Session Expired");
            }

            //validate the payment amount
            if (PaymentData.AmountToBePaid != createPaymentDTO.AmountPaid)
            {
                return BadRequest("Payment Amount Mismatch");
            }
            var transactionId = Guid.NewGuid();
            //update the payment status
            PaymentData.TransactionId = transactionId;
            PaymentData.PaymentStatus = Models.PaymentStatus.Completed;
            PaymentData.ExpiryTime = DateTime.Now;
            var PaymentStatus = await PaymentRepository.UpdatePayment(PaymentData);
            if (!PaymentStatus)
            {
                return BadRequest("Failed to update the payment status");
            }

            //update the checkout status
            CheckoutData.ExpiryTime = DateTime.Now;
            var CheckoutStatus = await CheckoutRepository.UpdateCheckoutSession(CheckoutData);
            if (!CheckoutStatus)
            {
                return BadRequest("Failed to update the checkout status");
            }
            //update the ticket counts
            foreach (var ticket in CheckoutData.Tickets)
            {
                await eventTicketRepository.UpdateTicketCount(ticket.TicketTypeId, ticket.Quantity);
            }
            //create a booking
            var BookingStatus = new Bookings()
            {
                AppUserID = user.Id,
                CheckoutSessionId = CheckoutData.Id,
                BookedAt = DateTime.Now
            };
            await BookingRepository.BookEvent(BookingStatus);
            return Ok(new
            {
                Id = BookingStatus.Id,
                TransactionID = transactionId,
                message = "Booking Successful"
            });
        }

        [Authorize]
        [HttpPut("UpdateCheckout/{checkoutID}/{paymentID}")]
        public async Task<IActionResult> UpdateCheckoutExpiry([FromRoute] Guid checkoutID, [FromRoute] Guid paymentID)
        {
            //get the user from the claims
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var checkoutData = await CheckoutRepository.GetCheckoutSession(checkoutID);
            if (checkoutData == null)
            {
                return NotFound("Checkout Session Not Found");
            }
            if (!checkoutData.IsCheckoutSessionExpired)
            {
                return BadRequest("Checkout Session Already valid");
            }

            var paymentData = await PaymentRepository.GetPaymentInformation(paymentID);
            if (paymentData == null)
            {
                return NotFound("Payment Session Not Found");
            }
            if (!paymentData.IsPaymentSessionExpired)
            {
                return BadRequest("Payment Session Already valid");
            }
            paymentData.ExpiryTime = DateTime.Now.AddMinutes(30);
            //update the expiry time
            checkoutData.ExpiryTime = DateTime.Now.AddMinutes(30);

            var updatedStatus = await CheckoutRepository.UpdateCheckoutSession(checkoutData);
            if (!updatedStatus)
            {
                return BadRequest("Failed to update the checkout session");
            }

            var updatedPaymentStatus = await PaymentRepository.UpdatePayment(paymentData);
            if (!updatedPaymentStatus)
            {
                return BadRequest("Failed to update the payment session");
            }
            return Ok(new
            {
                CheckoutID = checkoutData.Id,
                PaymentID = paymentData.Id
            });
        }
    }

}