using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Models;
using Ebooking.Models;

namespace Ebooking.Interface
{
    public interface IEventRepository
    {
        Task<List<Events>> GetAllEvents(QueryParamsDTO queryParams);
        Task<Events?> GetEventById(Guid id);

        Task<Events?> UpdateEventTicketCount(Guid guid, int tickets);
        Task<Events?> DeleteEvent(Guid guid);

        Task<Events> CreateEvent(Events events);
        Task<Events?> IncreaseEventTicketCount(Guid guid, int tickets);
        Task<List<EventCategory>> GetEventCategories();
    }
}