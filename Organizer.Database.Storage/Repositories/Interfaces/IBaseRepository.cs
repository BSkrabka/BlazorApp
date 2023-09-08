using System.Linq.Expressions;

namespace Organizer.Database.Storage.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task CreateAsync<TEntity>(TEntity entity) where TEntity : class;
    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
    Task UpdateAsync(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}