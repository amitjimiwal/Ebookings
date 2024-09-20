using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mapper;
using Ebooking.DTO.Bookings;
using Ebooking.Interface;
using Ebooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ebooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository BookingRepository;
        private readonly IEventRepository EventRepository;
        public BookingsController(IBookingRepository repository, IEventRepository eventRepository)
        {
            BookingRepository = repository;
            EventRepository = eventRepository;
        }
        [HttpPost]
        public async Task<IActionResult> BookEvent([FromBody] BookTicketDTO bookTicketDTO)
        {
            if (!ModelState.IsValid) //check if model state is valid
            {
                return BadRequest(ModelState);
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
            var BookingObject = bookTicketDTO.CreateBookingFromDTO();

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

        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> GetBookingById([FromRoute] Guid guid)
        {
            Bookings? BookingData = await BookingRepository.GetBookingByIDAsync(guid);

            if (BookingData == null) return NotFound("Can't Find Booking Data with the ID");

            //Convert it Into a DTO 
            return Ok(BookingData.CreateDTOFromBooking());
        }
    }
}