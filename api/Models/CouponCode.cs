using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ebooking.Models;

namespace api.Models
{
    public class CouponCode
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9-_]+$", ErrorMessage = "Code can only contain uppercase letters, numbers, hyphens and underscores")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        [Required]
        public DateTime Expiry { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxUsage { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CurrentUsage { get; set; }

        public Guid EventId { get; set; }
        public Events Event { get; set; }
    }
}