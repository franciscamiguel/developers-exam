using Domain.Entities;
using FluentValidation.Results;

namespace Domain.Interfaces.Services;

public interface IService<TEntity> where TEntity : Entity<TEntity>
{
    Task<ValidationResult> InsertAsync(TEntity entity);

    Task<ValidationResult> DeleteAsync(long id);

    Task<ValidationResult> UpdateAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(long id);

    Task<IEnumerable<TEntity>> GetAllAsync();
}