using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.DTO.Bookings;
using Ebooking.Models;

namespace api.Mapper
{
    public static class BookingMapper
    {
        public static Bookings CreateBookingFromDTO(this BookTicketDTO bookTicketDTO)
        {
            return new Bookings
            {
                Name = bookTicketDTO.Name,
                Email = bookTicketDTO.Email,
                PhoneNumber = bookTicketDTO.PhoneNumber,
                EventId = bookTicketDTO.EventId,
                NoOfTickets = bookTicketDTO.NoOfTickets,
                TotalPrice = bookTicketDTO.TotalPrice,
                BookedAt = DateTime.Now
            };
        }
    }
}