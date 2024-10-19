using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interface
{
    public interface IImageUploadService
    {
        Task<List<string>> UploadImageAsync(List<IFormFile> image,Guid eventId);
        Task<string> GetImageUrlAsync(string imageId);
        Task ValidateFile(IFormFile file);
    }
}