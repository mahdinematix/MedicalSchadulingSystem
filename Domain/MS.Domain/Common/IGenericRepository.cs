using System.Linq.Expressions;

namespace MS.Domain.Common;

public interface IGenericRepository<TKey, T> where T : class
{
    Task CreateAsync(T entity);
    void Update(T entity);
    Task<T> GetByIdAsync(TKey id);
    Task<List<T>> GetAllAsync();
    Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
    Task SaveChangesAsync();
}
