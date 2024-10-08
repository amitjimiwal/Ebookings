using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Ebooking.Data;
using Ebooking.Interface;
using Ebooking.Models;
using Microsoft.EntityFrameworkCore;

namespace Ebooking.Repository
{
    public class EventsRepository : IEventRepository
    {
        private readonly ApplicationDbContext Db;
        public EventsRepository(ApplicationDbContext dbContext)
        {
            Db = dbContext;
        }

        public async Task<Events> CreateEvent(Events events)
        {
            await Db.Events.AddAsync(events);
            await Db.SaveChangesAsync();
            return events;
        }

        public async Task<Events?> DeleteEvent(Guid guid)
        {
            var EventData = await Db.Events.FirstOrDefaultAsync(eve => eve.Id == guid);
            if (EventData == null)
            {
                return null;
            }

            Db.Events.Remove(EventData);
            return EventData;
        }

        public async Task<List<Events>> GetAllEvents()
        {
            var allEvents = await Db.Events.Include(cat => cat.Category).ToListAsync();
            return allEvents;
        }

        public async Task<Events?> GetEventById(Guid id)
        {
            var EventData = await Db.Events.Include(e => e.Category).FirstOrDefaultAsync(eve => eve.Id == id);
            if (EventData == null)
            {
                return null;
            }
            return EventData;
        }

        public async Task<Events?> UpdateEventTicketCount(Guid guid, int tickets)
        {
            var EventData = await Db.Events.FirstOrDefaultAsync(eve => eve.Id == guid);
            if (EventData == null)
            {
                return null;
            }
            if (EventData.AvailableTickets < tickets) return null;
            //reduce tickets and update the data
            EventData.AvailableTickets -= tickets;
            Db.Entry(EventData).Property(r => r.AvailableTickets).IsModified = true;

            await Db.SaveChangesAsync();
            return EventData;
        }
        public async Task<Events?> IncreaseEventTicketCount(Guid guid, int tickets)
        {
            var EventData = await Db.Events.FirstOrDefaultAsync(eve => eve.Id == guid);
            if (EventData == null)
            {
                return null;
            }
            if (EventData.AvailableTickets < tickets) return null;
            //reduce tickets and update the data
            EventData.AvailableTickets += tickets;
            Db.Entry(EventData).Property(r => r.AvailableTickets).IsModified = true;
            await Db.SaveChangesAsync();
            return EventData;
        }
    }
}