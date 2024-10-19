using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace api.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _ImageMAxSize = 5 * 1024 * 1024;


        private readonly IEventImageRepository eventImageRepository;
        public ImageUploadService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, IEventImageRepository eventImage)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            eventImageRepository = eventImage;
            //create a dir to store images
            var uploadsDir = Path.Combine(webHostEnvironment.ContentRootPath, "uploads", "events");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }
        }

        public Task<string> GetImageUrlAsync(string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> UploadImageAsync(List<IFormFile> image, Guid eventId)
        {
            var uploadedUrls = new List<string>();
            foreach (IFormFile file in image)
            {
                await ValidateFile(file);
                //id /extension
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
                var relativePath = Path.Combine("uploads", "events", fileName);
                var absolutePath = Path.Combine(webHostEnvironment.ContentRootPath, relativePath);
                // Save file
                using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string imageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/{relativePath.Replace("\\", "/")}";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                await eventImageRepository.UploadEventImage(eventId, imageUrl, file.Length);
                uploadedUrls.Add(imageUrl);
            }
            return uploadedUrls;
        }

        public Task<List<string>> UploadImageAsync(List<IFormFile> image)
        {
            throw new NotImplementedException();
        }

        public async Task ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }

            if (file.Length > _ImageMAxSize)
            {
                throw new ArgumentException($"File size exceeds the limit of {_ImageMAxSize / 1024 / 1024}MB");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Invalid file extension {extension}");
            }
        }
    }
}