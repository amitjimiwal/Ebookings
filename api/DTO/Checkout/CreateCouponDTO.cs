using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Checkout
{
    public class CreateCouponDTO
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9-_]+$", ErrorMessage = "Code can only contain uppercase letters, numbers, hyphens and underscores")]
        public string Code { get; set; } = string.Empty;

        [Required]
        public decimal DiscountPercentage { get; set; }

        [Required]
        public DateTime Expiry { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxUsage { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}