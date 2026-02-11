namespace Domain.Interfaces
{
  public interface IRepository<T> where T : class
  {
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int id);
    IQueryable<T> Query();
  }
}