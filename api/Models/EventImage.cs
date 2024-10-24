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
    public class EventImage
    {
        public Guid ID { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public Guid EventID { get; set; }

        [ForeignKey("EventID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Events Event { get; set; } //navigation property
    }
}