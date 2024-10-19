using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;

namespace api.Services
{
    public class EventImageRepository : IEventImageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EventImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EventImage> UploadEventImage(Guid eventId, string imageUrl, long size)
        {
            var eventImage = new EventImage
            {
                EventID = eventId,
                ImageUrl = imageUrl,
                Size = size
            };
            await _dbContext.EventImages.AddAsync(eventImage);
            await _dbContext.SaveChangesAsync();

            return eventImage;
        }
    }
}