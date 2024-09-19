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

        public async Task<List<Events>> GetAllEvents()
        {
            var allEvents = await Db.Events.Include(cat => cat.Category).ToListAsync();
            return allEvents;
        }
    }
}