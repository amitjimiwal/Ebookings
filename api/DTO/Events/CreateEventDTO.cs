using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Ebooking.Models;

namespace api.DTO.Events
{
    public class CreateEventDTO
    {

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }

        public string TimeZone { get; set; }
        public Venue Venue { get; set; }

        public string Description { get; set; }

        public int TotalTickets { get; set; }
        public int MaxTicketsPerAccount { get; set; }
        public decimal TicketPrice { get; set; }

        public List<IFormFile> Images { get; set; }
        public Guid CategoryId { get; set; }
        public List<CreateTicketDTO> TicketTypes { get; set; }
    }
}