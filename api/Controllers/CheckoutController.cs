using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Checkout;
using api.Interface;
using api.Mapper;
using Ebooking.Data;
using Ebooking.Interface;
using Ebooking.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        private readonly ApplicationDbContext DbContext;
        public CheckoutController(IEventRepository eventRepository, IBookingRepository bookingRepository, ApplicationDbContext dbContext, IEventTicketRepository eventTicket, ICouponCodeRepository couponCode)
        {
            EventRepository = eventRepository;
            BookingRepository = bookingRepository;
            DbContext = dbContext;
            eventTicketRepository = eventTicket;
            couponCodeRepository = couponCode;
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

            //get the event details
            var EventData = await EventRepository.GetEventById(createCheckoutSessionDTO.EventId);
            if (EventData == null)
            {
                return NotFound("Event Not Found");
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

            //create a payment session

            /*
            Send Back the response :{
                CheckoutSessionId,PaymentSessionID
            }
             */
            return Ok();
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

            return Ok(CouponData);
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