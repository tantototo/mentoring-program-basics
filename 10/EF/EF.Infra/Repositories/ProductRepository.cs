using EF.Infra.Context;
using EF.Infra.Entities;
using EF.Infra.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Product entity)
        {
            var result = _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            
            return result.Entity.ProductId;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id) => 
            await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
    }
}
