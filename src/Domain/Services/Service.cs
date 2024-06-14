using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using FluentValidation.Results;

namespace Domain.Services;

public abstract class Service<TEntity> : IService<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly IRepository<TEntity> _repository;

    public Service(IRepository<TEntity> repository)
        => _repository = repository;

    public virtual async Task<ValidationResult> InsertAsync(TEntity entity)
    {
        if (!entity.IsValid()) return entity.ValidationResult;

        await _repository.InsertAsync(entity);

        return null;
    }

    public virtual async Task<ValidationResult> DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);

        return null;
    }

    public virtual async Task<ValidationResult> UpdateAsync(TEntity entity)
    {
        if (!entity.IsValid()) return entity.ValidationResult;

        await _repository.UpdateAsync(entity);

        return null;
    }

    public async Task<TEntity> GetByIdAsync(long id)
        => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _repository.GetAllAsync();
}