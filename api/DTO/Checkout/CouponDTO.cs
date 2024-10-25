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
    }
}