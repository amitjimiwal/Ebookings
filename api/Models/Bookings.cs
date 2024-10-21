using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace Ebooking.Models
{
    [Table("Bookings")]
    public class Bookings
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AppUserID { get; set; }

        //Navigation Property
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

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        public Events Event { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalTicketsPurchased { get; set; }

        [Required]
        public DateTime BookedAt { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public  List<TicketInformation> TicketInformation { get; set; } = new List<TicketInformation>();

        public Guid PaymentInformationId { get; set; }
        public PaymentInformation PaymentInformation { get; set; } // Navigation property

        [NotMapped]
        public bool IsPaymentSessionExpired
        {
            get
            {
                return PaymentInformation.IsPaymentSessionExpired;
            }
        }
    }


    public class TicketInformation
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid TicketTypeId { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Expired
    }

}