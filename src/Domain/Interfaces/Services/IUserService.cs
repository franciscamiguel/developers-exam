using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IUserService : IService<User>
{
    Task<bool> GetByEmailAsync(string email);
    Task<bool> GetByLoginAsync(string login);
    Task<bool> GetUserAsync(string name, string surname);
}
