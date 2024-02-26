using EF.Infra.Context;
using EF.Infra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (result == null)
                return NotFound("Entity not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct(Product product)
        {
            var result = _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(result.Entity.ProductId);
        }

    }
}
