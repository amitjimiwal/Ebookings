using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.DTO.Events;
using Ebooking.Models;

namespace api.Mapper
{
    public static class EventsMapper
    {
        public static EventDTO CreateDTOFromEvent(this Events events)
        {
            return new EventDTO()
            {
                Id = events.Id,
                EventName = events.EventName,
                Date = events.Date,
                EventTiming = events.EventTiming,
                CreatedAt = events.CreatedAt,
                TotalTickets = events.TotalTickets,
                Description = events.Description,
                Venue = events.Venue,
                TicketPrice = events.TicketPrice,
                BannerImg = events.BannerImg,
                Category = events.Category.Name,
                AvailableTickets = events.AvailableTickets,
                MaxTicketsPerPerson = events.MaxTicketsPerPerson
            };
        }
    }
}