using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected SqlDbContext Context;
    protected DbSet<TEntity> DbSet;

    public Repository(SqlDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.SetLastAction();

        await DbSet.AddAsync(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(long id)
    {
        TEntity entity = await GetByIdAsync(id) ?? throw new ArgumentNullException("entity");
        DbSet.Remove(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity");

        entity.SetLastAction();
        DbSet.Update(entity);

        await Context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(long id)
        => await DbSet.FirstOrDefaultAsync(p => p.Id == id);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await DbSet.ToListAsync();

    public void Dispose()
        => Context.Dispose();

}