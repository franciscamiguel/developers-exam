using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task InsertAsync(TEntity entity);

    Task DeleteAsync(long id);

    Task UpdateAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(long id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}