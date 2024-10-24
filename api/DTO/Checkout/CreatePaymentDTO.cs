using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Checkout
{
    public class CreatePaymentDTO
    {
        [Required]
        public Guid CheckoutID { get; set; }

        [Required]
        public Guid PaymentID { get; set; }

        [Required]
        public Guid EventID { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public int TicketsBought { get; set; }
    }
}