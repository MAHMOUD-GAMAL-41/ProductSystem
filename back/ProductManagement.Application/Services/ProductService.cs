using Microsoft.AspNetCore.Http;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;


namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IImageService _imageService;

        public ProductService(IProductRepository repository , IImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }
       
        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductSearchDto searchDto)
        {
            var products = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchDto.Search))
            {
                products = products.Where(p => p.Name.Contains(searchDto.Search));
            }
            if (searchDto.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= searchDto.MinPrice.Value);
            }

            if (searchDto.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= searchDto.MaxPrice.Value);
            }
            var filteredProducts = products.ToList();

            return filteredProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedDate = p.CreatedDate,
                ImageUrl = p.ImageUrl
            });
        }


        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task AddAsync(ProductCreateDto productDto)
        {

            var imageUrl = await _imageService.SaveImageAsync(productDto.Image);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedDate = DateTime.Now,
                ImageUrl = imageUrl??""

            };
            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync( ProductUpdateDto productDto)
        {
            var product = await _repository.GetByIdAsync(productDto.Id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {productDto.Id} not found.");
            if (productDto.ImageUrl != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    _imageService.DeleteImageFromServer(product.ImageUrl); 
                }

                product.ImageUrl = await _imageService.SaveImageAsync(productDto.ImageUrl); 
            }
            product.Name = string.IsNullOrEmpty(productDto.Name) ? product.Name : productDto.Name;
            product.Description = string.IsNullOrEmpty(productDto.Description) ? product.Description : productDto.Description;
            product.Price = productDto.Price ?? product.Price;

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");

            await _repository.DeleteAsync(id);
        }
    }

}
