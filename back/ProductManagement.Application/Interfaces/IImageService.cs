using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(String image);
        void DeleteImageFromServer(string imageUrl);
    }
}
