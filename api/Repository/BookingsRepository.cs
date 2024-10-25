using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Data;
using Ebooking.Interface;
using Ebooking.Models;
using Microsoft.EntityFrameworkCore;

namespace Ebooking.Repository
{
    public class BookingsRepository : IBookingRepository
    {
        private readonly ApplicationDbContext Db;
        public BookingsRepository(ApplicationDbContext context)
        {
            Db = context;
        }
        public async Task<Bookings?> BookEvent(Bookings booking)
        {
            await Db.Bookings.AddAsync(booking);
            await Db.SaveChangesAsync();
            return booking;
        }

        public bool? DeleteAllBookingsForEvent(Guid eventID)
        {
            var BookingsForEventByUser = Db.Bookings.Include(b => b.CheckoutSession).AsQueryable();
            BookingsForEventByUser = BookingsForEventByUser.Where(e => e.CheckoutSession.EventId == eventID);
            if (BookingsForEventByUser == null) return false;
            //Delete EveryBooking for that event
            foreach (var book in BookingsForEventByUser)
            {
                Db.Bookings.Remove(book);
            }
            return true;
        }

        public async Task<bool> DeleteBooking(Guid bookingID)
        {
            var BookingData = await Db.Bookings.Where(item => item.Id == bookingID).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Status, BookingStatus.Cancelled));
            return true;
        }

        public async Task<Bookings?> GetBookingByIDAsync(Guid guid)
        {
            Bookings? BookingData = await Db.Bookings.Include(b => b.CheckoutSession).Include(b => b.CheckoutSession.PaymentInformation).Include(b => b.CheckoutSession.Event).FirstOrDefaultAsync(item => item.Id == guid);

            if (BookingData == null) return null;
            return BookingData;
        }

        public async Task<int> GetBookingsCount(Guid eventId, string appUserID)
        {
            //storing the data as queryable
            var BookingsForEventByUser = Db.Bookings.Include(b => b.CheckoutSession).AsQueryable();

            //filter out the bookings of user
            BookingsForEventByUser = BookingsForEventByUser.Where(booking => booking.AppLicationUser.Id == appUserID && booking.CheckoutSession.EventId == eventId && booking.Status == BookingStatus.Completed);

            //find the total sum od tickets purchased by the user
            var totalBookings = await BookingsForEventByUser.SumAsync(booking => booking.CheckoutSession.TotalTicketsPurchased);

            return totalBookings;
        }

        public async Task<List<Bookings>> GetBookingsForUser(string userId)
        {
            var bookingsData = Db.Bookings.Include(b => b.CheckoutSession).Include(b => b.CheckoutSession.PaymentInformation).Include(b => b.CheckoutSession.Event).AsQueryable();
            var b = await bookingsData.Where(x => x.AppUserID == userId && x.Status == BookingStatus.Completed).ToListAsync();
            return b;
        }
    }
}