using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using api.DTO.Bookings;

namespace Ebooking.DTO.Bookings
{
    public class BookTicketDTO
    {
        [Required]
        public Guid EventID { get; set; }

        [Required]
        public Guid AppUserId { get; set; }

        [Required]
        public Guid CheckoutId { get; set; }
    }
}