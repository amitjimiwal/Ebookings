using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Checkout
{
    public class CheckoutDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
        public string? CouponCode { get; set; }
        public decimal? DiscountAmount { get; set; }

        [Required]
        public decimal FinalAmount { get; set; }
        
        [Required]
        public ICollection<TicketDetails> Tickets { get; set; }
    }
}