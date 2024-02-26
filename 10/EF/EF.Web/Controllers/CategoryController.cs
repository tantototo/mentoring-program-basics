using EF.Infra.Context;
using EF.Infra.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var result = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
            if (result == null)
                return NotFound("Entity not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddCategory(Category category)
        {
            var result = _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(result.Entity.CategoryId);
        }
    }
}
