using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Bookings
{
    public class CancelBookingDTO
    {
        public Guid UserID { get; set; }

        public Guid BookingID { get; set; }

        public Guid EventID { get; set; }
    }
}