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
        [ForeignKey("AppUserID")]
        public ApplicationUser AppLicationUser { get; set; }

        // [Required]
        // public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        public DateTime BookedAt { get; set; } = DateTime.Now;

        public Guid CheckoutSessionId { get; set; }
        public CheckoutSession CheckoutSession { get; set; } // Navigation property
    }
}