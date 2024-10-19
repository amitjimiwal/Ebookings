using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Models;

namespace Ebooking.DTO.Events
{
    public class EventDTO
    {
        public Guid Id { get; set; }

        public string EventName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }

        public string TimeZone { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalTickets { get; set; }

        public string Description { get; set; } = string.Empty;

        public string VenueName { get; set; } = string.Empty;
        public string VenueAddress { get; set; } = string.Empty;

        public int VenueCapacity { get; set; }

        public decimal MinPriceTicket { get; set; }
        public int AvailableTickets { get; set; }
        public int? MaxTicketsPerPerson { get; set; }
        public string[]? Images { get; set; }
        public List<TicketDTO>? TicketTypes { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}