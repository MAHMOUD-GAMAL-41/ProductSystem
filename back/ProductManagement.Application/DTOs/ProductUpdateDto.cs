using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.DTOs
{
    public record ProductUpdateDto
    {
        public Guid Id { get; set; } 
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

    }
}
