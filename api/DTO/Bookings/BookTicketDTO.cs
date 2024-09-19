using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ebooking.DTO.Bookings
{
    public class BookTicketDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be atleast 3 characters")]
        [MaxLength(256, ErrorMessage = "Name must not exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        // [Phone]
        public long PhoneNumber { get; set; }

        [Required]

        public Guid EventId { get; set; }

        [Required]
        public int NoOfTickets { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
    }
}