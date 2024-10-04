using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookEvent([FromBody] BookTicketDTO bookTicketDTO)
        {
            if (!ModelState.IsValid) //check if model state is valid
            {
                return BadRequest(ModelState);
            }
            //get the user details
            string UserName = User.GetUserName();
            var appUser = await userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            //get event data
            var eventData = await EventRepository.GetEventById(bookTicketDTO.EventId);
            if (eventData == null)
            {
                return NotFound("Event Not Found");
            }
            //check if tickets greater than max allowed
            if (bookTicketDTO.NoOfTickets > eventData.MaxTicketsPerPerson)
            {
                return BadRequest("Tickets Exceeds Maximum Allowed");
            }
            //get booking count for the event by the person if already done
            int BookingCount = await BookingRepository.GetBookingsCount(bookTicketDTO.EventId, bookTicketDTO.Email);
            if (BookingCount >= eventData.MaxTicketsPerPerson)
            {
                return BadRequest("You Can't Book more tickets for this event.");
            }

            //check if tickets available
            if (bookTicketDTO.NoOfTickets > eventData.AvailableTickets)
            {
                return BadRequest("Sorry,Tickets Not Available");
            }

            //create booking object from DTO
            var BookingObject = bookTicketDTO.CreateBookingFromDTO(appUser.Id);

            //Update the events table available tickets
            var eventObj = await EventRepository.UpdateEventTicketCount(bookTicketDTO.EventId, bookTicketDTO.NoOfTickets);
            //book event
            var booking = await BookingRepository.BookEvent(BookingObject);

            //created at action: Get booking by id
            if (booking == null)
            {
                return StatusCode(500, "An error occurred while booking the event.");
            }
            return CreatedAtAction(nameof(GetBookingById), new { guid = booking.Id }, booking.CreateDTOFromBooking());
        }

        [Authorize]
        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> GetBookingById([FromRoute] Guid guid)
        {
            Bookings? BookingData = await BookingRepository.GetBookingByIDAsync(guid);

            if (BookingData == null) return NotFound("Can't Find Booking Data with the ID");

            //Convert it Into a DTO 
            return Ok(BookingData.CreateDTOFromBooking());
        }

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
            var bookingsDTO = bookings.Select(booking => booking.CreateDTOFromBooking());
            return Ok(bookingsDTO);
        }
    }
}