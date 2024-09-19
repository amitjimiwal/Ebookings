using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace Ebooking.DTO.Events
{
    public class EventDTO
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalTickets { get; set; }

        public string Description { get; set; }

        public string Venue { get; set; }
        public decimal TicketPrice { get; set; }

        public int AvailableTickets { get; set; }
        public int MaxTicketsPerPerson { get; set; }

        public string BannerImg { get; set; }

        // Foreign key

        // Navigation property
        public string Category { get; set; }
    }
}