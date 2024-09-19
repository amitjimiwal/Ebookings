using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace Ebooking.Models
{
    [Table("Events")]
    public class Events
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan EventTiming { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalTickets { get; set; }

        public string Description { get; set; }

        public string Venue { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketPrice { get; set; }

        public string BannerImg { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation property
        public EventCategory Category { get; set; }

        // Navigation property
        public ICollection<Bookings> Bookings { get; set; }
    }

}