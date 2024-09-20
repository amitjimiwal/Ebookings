using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.DTO.Events;
using Ebooking.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ebooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IBookingRepository BookingRepository;
        public EventsController(IEventRepository repository, IBookingRepository booking)
        {
            eventRepository = repository;
            BookingRepository = booking;
        }
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var EventsData = await eventRepository.GetAllEvents();
            if (EventsData == null)
            {
                return NotFound();
            }
            // Mapping the Events to EventDTO
            var EventsResponse = EventsData.Select(eve => new EventDTO()
            {
                Id = eve.Id,
                EventName = eve.EventName,
                Date = eve.Date,
                EventTiming = eve.EventTiming,
                CreatedAt = eve.CreatedAt,
                TotalTickets = eve.TotalTickets,
                Description = eve.Description,
                Venue = eve.Venue,
                TicketPrice = eve.TicketPrice,
                BannerImg = eve.BannerImg,
                Category = eve.Category.Name,
                AvailableTickets = eve.AvailableTickets,
                MaxTicketsPerPerson = eve.MaxTicketsPerPerson
            });
            return Ok(EventsResponse);
        }

        [HttpDelete]
        [Route("cancel/{guid}")]
        public async Task<IActionResult> CancelEvent(Guid guid)
        {
            var DeleteBookings = BookingRepository.DeleteAllBookingsForEvent(guid);
            var eventData = await eventRepository.DeleteEvent(guid);
            if (DeleteBookings == true && eventData != null) return Ok("Successfully deleted the event");
            return BadRequest("Error while deleting the Bookings for the event");
        }
    }
}