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
            Console.WriteLine("CreateCheckOutSession");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("Complete Validation");
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

            // validate the dto first - 1. check total price , total tickets (sum)
            if (!createCheckoutSessionDTO.IsDTOPriceAccurate() || !createCheckoutSessionDTO.IsDTOTicketsCountAccurate())
            {
                return BadRequest("DTO is not accurate in terms of price or tickets count");
            }
            //2. validate tickets with the actual event and ticket information
            foreach (var ticket in createCheckoutSessionDTO.Tickets)
            {
                var ticketInfo = await eventTicketRepository.GetTicketTypebyID(ticket.TicketId);

                //ticket not found check
                if (ticketInfo == null) return BadRequest($"{ticket.TicketDisplayName} Not Found");


                //invalid ticket id for the event check
                if (ticketInfo.EventId != createCheckoutSessionDTO.EventId) return BadRequest("Event doesn't have the provided ticketType");

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

            Console.WriteLine($"{user.Id}");
            var CheckoutStatus = await CheckoutRepository.CreateCheckoutSession(createCheckoutSessionDTO.CreateCheckoutFromDTO(user.Id));
            if (CheckoutStatus.Id.Equals(Guid.Empty))
            {
                return BadRequest("Failed to create a checkout session");
            }

            //create a payment session
            var PaymentStatus = await PaymentRepository.CreatePaymentInformation(CheckoutStatus.CreatePaymentSession());
            if (PaymentStatus.Id.Equals(Guid.Empty))
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
        [Route("/CouponCode")]
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
    }
}