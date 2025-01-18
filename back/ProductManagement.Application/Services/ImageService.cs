using Microsoft.AspNetCore.Http;
using ProductManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

        public async Task<string> SaveImageAsync(string image)
        {
            if (image == null) return null;

            if (!Directory.Exists(_imageDirectoryPath))
                Directory.CreateDirectory(_imageDirectoryPath);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(_imageDirectoryPath, fileName);

            var imageBytes = Convert.FromBase64String(image.Split(',')[1]); 
            await File.WriteAllBytesAsync(filePath, imageBytes);

            return $"/images/products/{fileName}";
        }

        public void DeleteImageFromServer(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

}
