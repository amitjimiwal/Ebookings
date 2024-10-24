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
    public class CheckoutSession
    {
        [Required]
        public Guid Id { get; set; }

        // User who created the checkout session

        [Required]
        public string AppUserID { get; set; }

        //Navigation Property
        [ForeignKey("AppUserID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ApplicationUser AppLicationUser { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        // Event for which the checkout session is created
        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Events Event { get; set; }

        //ticket purchase and amount information
        [Required]
        [Range(1, int.MaxValue)]
        public int TotalTicketsPurchased { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string? CouponCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        //status of the checkout session
        [Required]
        public CheckoutStatus Status { get; set; } = CheckoutStatus.Pending;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime ExpiryTime { get; set; } = DateTime.Now.AddMinutes(30);


        //utility function
        [NotMapped]
        public bool IsCheckoutSessionExpired => DateTime.Now > ExpiryTime;
        public PaymentInformation PaymentInformation { get; set; }
        public ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
    }
    public class Tickets
    {
        [Required]
        public Guid TicketTypeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string TicketName { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public enum CheckoutStatus
    {
        Pending,
        Paid,
        Cancelled
    }
}