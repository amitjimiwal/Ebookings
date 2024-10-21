using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace Ebooking.Models
{
    [Table("Events")]
    public class Events
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }

        public string TimeZone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalTickets { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public Venue Venue { get; set; }


        [NotMapped]  // For querying purposes only , not mapped to the database
        public int AvailableTickets
        {
            get
            {
                return TicketTypes.Sum(ticket => ticket.AvailableTickets);
            }
        }

        [NotMapped]
        public decimal MinTicketPrice
        {
            get
            {
                return TicketTypes.Min(ticket => ticket.AvailableTickets > 0 ? ticket.TicketPrice : decimal.MaxValue);
            }
        }
        public int MaxTicketsPerAccount { get; set; }

        // for user creating events
        public string ApplicationUserID { get; set; }

        public ApplicationUser applicationUser { get; set; }

        // Navigation property
        // Foreign key
        public Guid CategoryId { get; set; }

        // Navigation property
        public EventCategory Category { get; set; }

        // Navigation property
        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>(); //navigation property for bookings

        public ICollection<EventImage> EventImages { get; set; } = new List<EventImage>(); //navigation property for images

        public ICollection<TicketTypes> TicketTypes { get; set; } = new List<TicketTypes>();//navigation property for ticket types

        public ICollection<CouponCode> EventCouponCodes { get; set; } = new List<CouponCode>(); //navigation property for coupon codes
    }
    public class Venue
    {
        [Required]
        [MaxLength(255)]  // Venue name length
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]  // Venue address length
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]  // State length
        public string State { get; set; }

        [Required]
        public int Capacity { get; set; }
    }

}

