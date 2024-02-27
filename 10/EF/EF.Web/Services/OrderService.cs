using EF.Infra.Repositories.Infrastructure;
using EF.Web.Services.Infrastructure;

namespace EF.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<int> AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
