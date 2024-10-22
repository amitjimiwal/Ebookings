using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Bookings;
using api.Models;
using Ebooking.DTO.Bookings;
using Ebooking.Models;

namespace api.Mapper
{
    public static class BookingMapper
    {
        public static Bookings CreateBookingFromDTO(this BookTicketDTO bookTicketDTO, string userId, Guid CheckoutSessionId)
        {
            return new Bookings
            {
                AppUserID = userId,
                CheckoutSessionId = CheckoutSessionId,
                BookedAt = DateTime.Now,
            };
        }

        public static BookingDTO CreateDTOFromBooking(this Bookings bookings)
        {
            return new BookingDTO
            {
                Id = bookings.Id,
                Name = bookings.CheckoutSession.Name,
                Email = bookings.CheckoutSession.Email,
                PhoneNumber = bookings.CheckoutSession.PhoneNumber,
                EventId = bookings.CheckoutSession.EventId,
                NoOfTickets = bookings.CheckoutSession.TotalTicketsPurchased,
                TotalPrice = bookings.CheckoutSession.FinalAmount,
                BookedAt = bookings.BookedAt,
                AppUserID = bookings.AppUserID
            };
        }

        public static UserBookingDTO CreateUserBookingDTOFromBooking(this Bookings bookings)
        {
            return new UserBookingDTO
            {
                Id = bookings.Id,
                Name = bookings.CheckoutSession.Name,
                Email = bookings.CheckoutSession.Email,
                PhoneNumber = bookings.CheckoutSession.PhoneNumber,
                EventId = bookings.CheckoutSession.EventId,
                NoOfTickets = bookings.CheckoutSession.TotalTicketsPurchased,
                TotalPrice = bookings.CheckoutSession.FinalAmount,
                BookedAt = bookings.BookedAt,
                AppUserID = bookings.AppUserID,
                EventName = bookings.CheckoutSession.Event.EventName,
                EventLocation = bookings.CheckoutSession.Event.Venue.Address,
                BannerImages = bookings.CheckoutSession.Event.EventImages.Select(image => image.ImageUrl).ToList(),
                Description = bookings.CheckoutSession.Event.Description,
                EventDate = bookings.CheckoutSession.Event.Date
            };
        }
    }
}