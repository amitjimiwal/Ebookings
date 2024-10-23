using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Checkout
{
    public class CreateCheckoutSessionDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required]
        public ICollection<TicketDetails> Tickets { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string? CouponCode { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public bool IsDTOPriceAccurate()
        {
            Console.WriteLine(FinalAmount);
            return FinalAmount == Tickets.Sum(x => x.TicketQuantity * x.SingleticketPrice);
        }
        public bool IsDTOTicketsCountAccurate()
        {
            Console.WriteLine(Tickets.Sum(x => x.TicketQuantity));
            return TotalTickets == Tickets.Sum(x => x.TicketQuantity);
        }
    }
    public class TicketDetails
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        public int TicketQuantity { get; set; }

        [Required]
        public decimal SingleticketPrice { get; set; }

        [Required]
        public string TicketDisplayName { get; set; } = string.Empty;
    }
}