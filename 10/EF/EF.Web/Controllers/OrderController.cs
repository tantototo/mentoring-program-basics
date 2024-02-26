using EF.Infra.Context;
using EF.Infra.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrderController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var result = await _context.Orders
                .Include(p => p.Customer)
                .Include(p => p.OrderDetails)
                .ThenInclude(d => d.Product)
                .ToListAsync();
            return Ok(result);
        }
    }
}
