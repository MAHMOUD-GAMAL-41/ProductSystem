using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using ProductManagement.Application.Validation;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Database;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.WebAPI.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAntiforgery();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                       builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductSearchDtoValidator>();
        }
    }
}
