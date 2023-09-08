using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Organizer.Database.Storage.Repositories.Interfaces;

namespace Organizer.Database.Storage.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly OrganizerDbContext Context;

    public BaseRepository(OrganizerDbContext context)
    {
        Context = context;
    }

    public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        return await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task BeginTransactionAsync()
    {
        await Context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await Context.Database.CurrentTransaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await Context.Database.CurrentTransaction.RollbackAsync();
    }
}