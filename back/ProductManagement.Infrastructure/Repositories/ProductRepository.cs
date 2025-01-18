using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Product>> GetAllAsync() => _context.Products;

        public async Task<Product> GetByIdAsync(Guid id) => await _context.Products.FirstOrDefaultAsync(x=>x.Id==id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }

}
