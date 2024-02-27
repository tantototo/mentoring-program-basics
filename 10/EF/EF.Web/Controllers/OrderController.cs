using EF.Web.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all orders with details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
    }
}
