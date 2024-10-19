using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface IEventImageRepository
    {
        Task<EventImage> UploadEventImage(Guid eventId, string imageUrl, long size);
    }
}