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
    }
}