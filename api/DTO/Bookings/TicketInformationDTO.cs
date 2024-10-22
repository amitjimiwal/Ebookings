using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Bookings
{
    public class TicketInformationDTO
    {
        [Required]
        public Guid TicketTypeID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}