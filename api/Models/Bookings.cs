using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace Ebooking.Models
{
    [Table("Bookings")]
    public class Bookings
    {
        public Guid Id { get; set; }
        public string AppUserID { get; set; }

        //Navigation Property
        public ApplicationUser AppLicationUser { get; set; }
        // Foreign key
        public string Name { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }
        public Guid EventId { get; set; }

        public Events Event { get; set; }

        public int NoOfTickets { get; set; }

        public DateTime BookedAt { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
    }

}