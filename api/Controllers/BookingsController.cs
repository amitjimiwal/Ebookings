using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mapper;
using Ebooking.DTO.Bookings;
using Ebooking.Interface;
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
            //TODO: get booking count for the event by the person

            //check if tickets available
            if (bookTicketDTO.NoOfTickets > eventData.AvailableTickets)
            {
                return BadRequest("Sorry,Tickets Not Available");
            }

            //create booking object from DTO
            var BookingObject = bookTicketDTO.CreateBookingFromDTO();

            //TODO: Update the events table available tickets

            //book event
            var booking = await BookingRepository.BookEvent(BookingObject);

            //TODO: created at action: Get booking by id
            return Ok(booking);
        }
    }
}