using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Bookings;
using api.Mapper;
using api.Models;
using Ebooking.DTO.Bookings;
using Ebooking.Interface;
using Ebooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockapi.Extensions;

namespace Ebooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository BookingRepository;
        private readonly IEventRepository EventRepository;

        private readonly UserManager<ApplicationUser> userManager;
        public BookingsController(IBookingRepository repository, IEventRepository eventRepository, UserManager<ApplicationUser> manager)
        {
            BookingRepository = repository;
            EventRepository = eventRepository;
            userManager = manager;
        }

        // [Authorize]
        // [HttpPost]
        // public async Task<IActionResult> BookEvent([FromBody] BookTicketDTO bookTicketDTO)
        // {
        //     if (!ModelState.IsValid) //check if model state is valid
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     //get the user details
        //     string UserName = User.GetUserName();
        //     var appUser = await userManager.FindByNameAsync(UserName);
        //     if (appUser == null)
        //     {
        //         return BadRequest("User not found");
        //     }
        //     //get event data
        //     var eventData = await EventRepository.GetEventById(bookTicketDTO.EventId);
        //     if (eventData == null)
        //     {
        //         return NotFound("Event Not Found");
        //     }
        //     //check if tickets greater than max allowed
        //     if (bookTicketDTO.NoOfTickets > eventData.MaxTicketsPerAccount)
        //     {
        //         return BadRequest("Tickets Exceeds Maximum Allowed");
        //     }
        //     //get booking count for the event by the person if already done prior
        //     int BookingCount = await BookingRepository.GetBookingsCount(bookTicketDTO.EventId, appUser.Id);
        //     if (BookingCount >= eventData.MaxTicketsPerAccount)
        //     {
        //         return BadRequest("You Can't Book more tickets for this event.");
        //     }

        //     //check if tickets available
        //     if (bookTicketDTO.NoOfTickets > eventData.AvailableTickets)
        //     {
        //         return BadRequest("Sorry,Tickets Not Available");
        //     }

        //     //create booking object from DTO
        //     var BookingObject = bookTicketDTO.CreateBookingFromDTO(appUser.Id, new Guid());

        //     //Update the events table available tickets
        //     var eventObj = await EventRepository.UpdateEventTicketCount(bookTicketDTO.EventId, bookTicketDTO.NoOfTickets);
        //     //book event
        //     var booking = await BookingRepository.BookEvent(BookingObject);

        //     //created at action: Get booking by id
        //     if (booking == null)
        //     {
        //         return StatusCode(500, "An error occurred while booking the event.");
        //     }
        //     return CreatedAtAction(nameof(GetBookingById), new { guid = booking.Id }, booking.CreateDTOFromBooking());
        // }

        // [Authorize]
        // [HttpGet]
        // [Route("{guid}")]
        // public async Task<IActionResult> GetBookingById([FromRoute] Guid guid)
        // {
        //     Bookings? BookingData = await BookingRepository.GetBookingByIDAsync(guid);

        //     if (BookingData == null) return NotFound("Can't Find Booking Data with the ID");

        //     //Convert it Into a DTO 
        //     return Ok(BookingData.CreateDTOFromBooking());
        // }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBookingsForUser()
        {
            //get the username from claims
            string UserName = User.GetUserName();
            var appUser = await userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            //get all bookings for the user
            var bookings = await BookingRepository.GetBookingsForUser(appUser.Id);
            //traverse and print
            if (bookings == null)
            {
                return NotFound("No Bookings Found");
            }
            //convert to DTO
            var bookingsDTO = bookings.Select(booking => booking.CreateUserBookingDTOFromBooking());
            return Ok(bookingsDTO);
        }

        [Authorize]

        [HttpDelete("cancel/{cancelBookingID}")]
        public async Task<IActionResult> CancelBooking([FromRoute] Guid cancelBookingID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string UserName = User.GetUserName();
            var appUser = await userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }

            //get the booking details
            var bookingData = await BookingRepository.GetBookingByIDAsync(cancelBookingID);
            if (bookingData == null)
            {
                return NotFound("Booking Not Found");
            }

            //check if the booking is done by the user
            if (bookingData.AppUserID != appUser.Id)
            {
                return BadRequest("You can't cancel this booking.");
            }


            //get the event details
            var eventData = await EventRepository.GetEventById(bookingData.CheckoutSession.EventId);
            if (eventData == null)
            {
                return NotFound("Event Not Found");
            }

            //check if current date and event date has greater than 2 days difference
            if (DateTime.Now.Subtract(eventData.Date).Days > 2)
            {
                return BadRequest("Can't Cancel Booking 2 days before the event.");
            }

            //delete the booking 
            var bookingDeleted = BookingRepository.DeleteBooking(cancelBookingID);
            if (bookingDeleted == false)
            {
                return StatusCode(500, "An error occurred while deleting the booking.");
            }
            //update the event available tickets
            // var eventObj = await EventRepository.IncreaseEventTicketCount(bookingData.EventId, bookingData.NoOfTickets);
            // if (eventObj == null)
            // {
            //     return StatusCode(500, "An error occurred while updating the event tickets.");
            // }
            return Ok();
        }
    }
}