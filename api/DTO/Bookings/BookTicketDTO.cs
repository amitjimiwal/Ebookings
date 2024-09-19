using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ebooking.DTO.Bookings
{
    public class BookTicketDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public Guid EventId { get; set; }
        public int NoOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
    }
}