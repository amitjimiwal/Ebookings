using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Ebooking.Models;

namespace api.DTO.Events
{
    public class CreateEventDTO
    {
        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan EventTiming { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public Venue Venue { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MaxTicketsPerAccount { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateTicketDTO> TicketTypes { get; set; } = new List<CreateTicketDTO>();
    }
}