namespace EF.Infra.Repositories.Infrastructure
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
    }
}
