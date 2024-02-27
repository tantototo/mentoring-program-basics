namespace EF.Web.Services.Infrastructure
{
    public interface IBaseService<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
    }
}
