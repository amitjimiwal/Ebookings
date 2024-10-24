using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Models;
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

        public async Task<List<Events>> GetAllEvents(QueryParamsDTO query)
        {
            var allEvents = Db.Events.Include(eve => eve.Category).Include(eve => eve.EventImages).Include(eve => eve.TicketTypes).AsQueryable();

            //search by name or venue
            if (!string.IsNullOrWhiteSpace(query.SearchTopic) && !string.IsNullOrWhiteSpace(query.SearchQuery))
            {
                if (query.SearchTopic.Equals("EventName", StringComparison.OrdinalIgnoreCase))
                {
                    allEvents = allEvents.Where(e => e.EventName.Contains(query.SearchQuery));
                };
                if (query.SearchTopic.Equals("Venue", StringComparison.OrdinalIgnoreCase))
                {
                    allEvents = allEvents.Where(e => e.Venue.Name.Contains(query.SearchQuery));
                };
            }
            //filter by category
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                allEvents = allEvents.Where(e => e.Category.Name == query.Category);
            }
            if (!string.IsNullOrWhiteSpace(query.SortBY))
            {
                //sort by Symbol
                if (query.SortBY.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    allEvents = query.IsDescending ? allEvents.OrderByDescending(s => s.Date) : allEvents.OrderBy(s => s.Date);
                };
                //sort by CompanyName
                if (query.SortBY.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    allEvents = query.IsDescending ? allEvents.OrderByDescending(s => s.TicketTypes.Min(ticket => ticket.AvailableTickets > 0 ? ticket.TicketPrice : decimal.MaxValue)) : allEvents.OrderBy(s => s.TicketTypes.Min(ticket => ticket.AvailableTickets > 0 ? ticket.TicketPrice : decimal.MaxValue));
                };
            }
            return await allEvents.ToListAsync();
        }

        public async Task<Events?> GetEventById(Guid id)
        {
            var EventData = await Db.Events.Include(e => e.Category).Include(eve => eve.EventImages).Include(eve => eve.TicketTypes).FirstOrDefaultAsync(eve => eve.Id == id);
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
            // EventData. -= tickets;
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
            // EventData.AvailableTickets += tickets;
            Db.Entry(EventData).Property(r => r.AvailableTickets).IsModified = true;
            await Db.SaveChangesAsync();
            return EventData;
        }

        public async Task<List<EventCategory>> GetEventCategories()
        {
            var categories = await Db.EventCategories.ToListAsync();
            return categories;
        }
    }
}