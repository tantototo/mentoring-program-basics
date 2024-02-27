using EF.Infra.Repositories.Infrastructure;
using EF.Web.Services.Infrastructure;

namespace EF.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<int> AddAsync(Category entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task<IList<Category>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
    }
}
