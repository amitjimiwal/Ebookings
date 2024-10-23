using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Checkout
{
    public class CouponDTO
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime Expiry { get; set; }
        public int MaxUsage { get; set; }
        public int CurrentUsage { get; set; }
        public Guid EventId { get; set; }
    }
}