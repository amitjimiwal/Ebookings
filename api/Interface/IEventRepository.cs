using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Models;

namespace Ebooking.Interface
{
    public interface IEventRepository
    {
        Task<List<Events>> GetAllEvents();
        Task<Events?> GetEventById(Guid id);

        Task<Events?> UpdateEventTicketCount(Guid guid,int tickets);
    }
}