using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Interface;
using api.Models;
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
                VenueName = events.Venue.Name,
                VenueAddress = events.Venue.Address,
                VenueCapacity = events.Venue.Capacity,
                MinPriceTicket = events.MinTicketPrice,
                Images = events.EventImages.Select(image => image.ImageUrl).ToArray(),
                Category = events.Category.Name,
                AvailableTickets = events.AvailableTickets,
                MaxTicketsPerPerson = events.MaxTicketsPerAccount,
                TicketTypes = events.TicketTypes.Select(ticket => new TicketDTO()
                {
                    Id = ticket.Id,
                    EventID = ticket.EventId,
                    TicketPrice = ticket.TicketPrice,
                    TotalTickets = ticket.TotalTickets,
                    AvailableTickets = ticket.AvailableTickets,
                    DisplayName = ticket.DisplayName
                }).ToList()
            };
        }
        public static Events CreateEventFromDTO(this CreateEventDTO createEventDTO, string userID)
        {
            return new Events()
            {
                EventName = createEventDTO.EventName,
                Date = createEventDTO.Date,
                EventTiming = createEventDTO.EventTiming,
                TotalTickets = createEventDTO.Venue.Capacity,
                TimeZone = createEventDTO.TimeZone,
                Description = createEventDTO.Description,
                Venue = createEventDTO.Venue,
                MaxTicketsPerAccount = createEventDTO.MaxTicketsPerAccount,
                CategoryId = createEventDTO.CategoryId,
                AppUserID = userID
            };
        }
    }
}