using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Models;

namespace Ebooking.Interface
{
    public interface IBookingRepository
    {
        Task<Bookings?> BookEvent(Bookings booking);
        Task<int> GetBookingsCount(Guid eventId, string email);

        Task<Bookings?> GetBookingByIDAsync(Guid guid);

        bool? DeleteAllBookingsForEvent(Guid eventID);
        Task<List<Bookings>> GetBookingsForUser(string userId);
        Task<bool> DeleteBooking(Guid bookingID);
    }
}