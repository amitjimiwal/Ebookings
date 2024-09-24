using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Bookings;
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

        public static BookingDTO CreateDTOFromBooking(this Bookings bookings)
        {
            return new BookingDTO
            {
                Id = bookings.Id,
                Name = bookings.Name,
                Email = bookings.Email,
                PhoneNumber = bookings.PhoneNumber,
                EventId = bookings.EventId,
                NoOfTickets = bookings.NoOfTickets,
                TotalPrice = bookings.TotalPrice,
                BookedAt = bookings.BookedAt,
            };
        }
    }
}