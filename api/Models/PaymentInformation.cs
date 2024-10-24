using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{

    public class PaymentInformation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountToBePaid { get; set; }

        public Guid TransactionId { get; set; }
        public string PaymentMethod { get; set; }

        [Required]
        public string Currency { get; set; } = "INR";

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DateTime ExpiryTime { get; set; } = DateTime.Now.AddMinutes(15);

        [NotMapped]
        public bool IsPaymentSessionExpired
        {
            get
            {
                return ExpiryTime < DateTime.Now;
            }
        }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CheckoutSessionId { get; set; }

        [ForeignKey("CheckoutSessionId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CheckoutSession CheckoutSession { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
}