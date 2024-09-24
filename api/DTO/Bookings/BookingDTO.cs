using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Bookings
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public Guid EventId { get; set; }
        public int NoOfTickets { get; set; }
        public DateTime BookedAt { get; set; }
        public decimal TotalPrice { get; set; }
    }
}