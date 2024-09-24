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
            var BookingsForEventByUser = Db.Bookings.AsQueryable();
            BookingsForEventByUser = BookingsForEventByUser.Where(e => e.EventId == eventID);
            if (BookingsForEventByUser == null) return false;
            //Delete EveryBooking for that event
            foreach (var book in BookingsForEventByUser)
            {
                Db.Bookings.Remove(book);
            }
            return true;
        }

        public async Task<Bookings?> GetBookingByIDAsync(Guid guid)
        {
            Bookings? BookingData = await Db.Bookings.FirstOrDefaultAsync(item => item.Id == guid);

            if (BookingData == null) return null;
            return BookingData;
        }

        public async Task<int> GetBookingsCount(Guid eventId, string email)
        {
            //storing the data as queryable
            var BookingsForEventByUser = Db.Bookings.AsQueryable();

            //filter out the bookings of user
            BookingsForEventByUser = BookingsForEventByUser.Where(booking => booking.EventId == eventId);

            return await BookingsForEventByUser.CountAsync();
        }
    }
}