using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTO.Events
{
    public class CreateEventDTO
    {
        
        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }
        public int TotalTickets { get; set; }

        public string Description { get; set; }

        public string Venue { get; set; }

        public decimal TicketPrice { get; set; }

        public string BannerImg { get; set; }
        public int MaxTicketsPerPerson { get; set; }
        public Guid CategoryId { get; set; }
    }
}