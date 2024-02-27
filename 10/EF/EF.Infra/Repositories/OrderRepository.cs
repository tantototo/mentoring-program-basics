using EF.Infra.Context;
using EF.Infra.Entities;
using EF.Infra.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _context;

        public OrderRepository(NorthwindContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(p => p.Customer)
                .Include(p => p.OrderDetails)
                .ThenInclude(d => d.Product)
                .ToListAsync();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
