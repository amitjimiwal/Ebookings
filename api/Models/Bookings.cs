using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ebooking.Models
{
    [Table("Bookings")]
    public class Bookings
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public long PhoneNumber { get; set; }

    // Foreign key
    public int EventId { get; set; }

    public Events Event { get; set; }

    public int NoOfTickets { get; set; }

    public DateTime BookedAt { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }
}

}