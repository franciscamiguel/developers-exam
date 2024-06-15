using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class Repository<TEntity>(SqlDbContext context) : IDisposable, IRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected SqlDbContext Context = context;
    protected DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task InsertAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.SetLastAction();

        await DbSet.AddAsync(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(long id)
    {
        TEntity entity = await GetByIdAsync(id)
            ?? throw new ArgumentNullException("entity");

        DbSet.Remove(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.SetLastAction();
        DbSet.Update(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(long id)
        => await DbSet.FirstOrDefaultAsync(p => p.Id == id);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await DbSet.ToListAsync();

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await DbSet.AnyAsync(predicate);

    public void Dispose()
        => Context.Dispose();

}