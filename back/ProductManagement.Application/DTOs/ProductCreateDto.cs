
using Microsoft.AspNetCore.Http;

namespace ProductManagement.Application.DTOs
{
    public record ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Image { get; set; }

    }
}
