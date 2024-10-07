using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Bookings
{
    public class UserBookingDTO : BookingDTO
    {
        public string? EventName { get; set; } = string.Empty;
        public string? EventLocation { get; set; } = string.Empty;
        public DateTime? EventDate { get; set; }
        public string? Description { get; set; } = string.Empty;

        public string? BannerImg { get; set; } = string.Empty;
    }
}