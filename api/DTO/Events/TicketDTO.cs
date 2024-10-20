using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Events
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public Guid EventID { get; set; }
        public decimal TicketPrice { get; set; }

        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}