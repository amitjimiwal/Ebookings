using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Checkout;
using api.Models;

namespace api.DTO.Bookings
{
    public class UserBookingDTO : BookingDTO
    {
        public string? EventName { get; set; } = string.Empty;
        public string? EventLocation { get; set; } = string.Empty;
        public DateTime? EventDate { get; set; }
        public string? Description { get; set; } = string.Empty;

        public string? CouponCodeApplied { get; set; }=null;
        public decimal DiscountPercentage { get; set; }
        public List<Tickets> Tickets { get; set; } = new List<Tickets>();
    }
}