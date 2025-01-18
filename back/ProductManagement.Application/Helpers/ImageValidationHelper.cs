using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Helpers
{
    public static class ImageValidationHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxImageSize = 5 * 1024 * 1024; 

     

        public static bool IsValidImageType(IFormFile image)
        {
            if (image == null) return false;

            var extension = Path.GetExtension(image.FileName)?.ToLower();
            return AllowedExtensions.Contains(extension);
        }

        public static bool IsValidImageSize(IFormFile image)
        {
            if (image == null) return false;

            return image.Length <= MaxImageSize;
        }
    }
}
