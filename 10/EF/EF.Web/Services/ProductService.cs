using EF.Infra.Repositories.Infrastructure;
using EF.Web.Services.Infrastructure;

namespace EF.Web.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _repositopy;

        public ProductService(IProductRepository repositopy)
        {
            _repositopy = repositopy;
        }

        public Task<int> AddAsync(Product entity)
        {
            return _repositopy.AddAsync(entity);
        }

        public Task<IList<Product>> GetAllAsync()
        {
            return _repositopy.GetAllAsync();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _repositopy.GetByIdAsync(id);
        }
    }
}
