using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Bookings
{
    public class CancelBookingDTO
    {
        public Guid BookingID { get; set; }
    }
}