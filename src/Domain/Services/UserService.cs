using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class UserService(IRepository<User> repository) : Service<User>(repository), IUserService
{
    public async Task<bool> GetByEmailAsync(string email)
        => await repository.ExistsAsync(u => u.Email == email);

    public async Task<bool> GetByLoginAsync(string login)
        => await repository.ExistsAsync(u => u.Login == login);

    public async Task<bool> GetUserAsync(string name, string surname)
        => await repository.ExistsAsync(u => u.Name == name && u.Surname == surname);
}
