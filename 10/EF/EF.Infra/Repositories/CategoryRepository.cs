using EF.Infra.Context;
using EF.Infra.Entities;
using EF.Infra.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _context;

        public CategoryRepository(NorthwindContext context) 
        {
            _context = context;
        }

        public async Task<int> AddAsync(Category entity)
        {
            var result = _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return result.Entity.CategoryId;
        }

        public async Task<IList<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id) => 
            await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
    }
}
