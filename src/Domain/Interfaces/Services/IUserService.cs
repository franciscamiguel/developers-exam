using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Services;

public interface IUserService : IService<User>
{
    Task<bool> GetByEmailAsync(string email);
}
