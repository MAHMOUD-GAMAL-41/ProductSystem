using ProductManagement.Application.DTOs;


namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductSearchDto searchDto);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task AddAsync(ProductCreateDto productDto);
        Task UpdateAsync(ProductUpdateDto productDto);
        Task DeleteAsync(Guid id);
    }

}
