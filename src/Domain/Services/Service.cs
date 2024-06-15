using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace Domain.Services;

public abstract class Service<TEntity>(IRepository<TEntity> repository) : IService<TEntity> where TEntity : Entity<TEntity>
{
    public virtual async Task<ValidationResult> InsertAsync(TEntity entity)
    {
        if (!entity.IsValid()) return entity.ValidationResult;

        await repository.InsertAsync(entity);
        return null;
    }

    public virtual async Task<ValidationResult> DeleteAsync(long id)
    {
        await repository.DeleteAsync(id);
        return null;
    }

    public virtual async Task<ValidationResult> UpdateAsync(TEntity entity)
    {
        if (!entity.IsValid()) return entity.ValidationResult;

        await repository.UpdateAsync(entity);

        return null;
    }

    public async Task<TEntity> GetByIdAsync(long id)
        => await repository.GetByIdAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await repository.GetAllAsync();

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await repository.ExistsAsync(predicate);
}