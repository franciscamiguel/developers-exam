using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class UserService(IRepository<User> repository) : Service<User>(repository), IUserService
{
    public async Task<bool> GetByEmailAsync(string email)
    {
        var users = await repository.GetAllAsync();

        return users.Any(u => u.Email == email);
    }
}
