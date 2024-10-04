using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
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
        public static Events CreateEventFromDTO(this CreateEventDTO createEventDTO, string userID)
        {
            return new Events()
            {
                EventName = createEventDTO.EventName,
                Date = createEventDTO.Date,
                EventTiming = createEventDTO.EventTiming,
                TotalTickets = createEventDTO.TotalTickets,
                Description = createEventDTO.Description,
                Venue = createEventDTO.Venue,
                TicketPrice = createEventDTO.TicketPrice,
                BannerImg = createEventDTO.BannerImg,
                AvailableTickets = createEventDTO.TotalTickets,
                MaxTicketsPerPerson = createEventDTO.MaxTicketsPerPerson,
                CategoryId = createEventDTO.CategoryId,
                // Assigning the user id to the event
                ApplicationUserID = userID
            };
        }
    }
}