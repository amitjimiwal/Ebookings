using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Models;

namespace api.Models
{
    public class TicketTypes
    {
        public int Id { get; set; }

        // Foreign key relationship with the Events table
        public int EventId { get; set; }
        public Events Event { get; set; }  // Navigation property for the related Event

        public decimal TicketPrice { get; set; }

        public int Quantity { get; set; }

        public string DisplayName { get; set; }
    }
}