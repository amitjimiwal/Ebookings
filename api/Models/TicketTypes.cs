using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Models;

namespace api.Models
{
    public class TicketTypes
    {
        public int Id { get; set; }

        // Foreign key relationship with the Events table
        public int TicketEventID { get; set; }
        public Events Event { get; set; }  // Navigation property for the related Event

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketPrice { get; set; }

        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }

        public string DisplayName { get; set; }
    }
}