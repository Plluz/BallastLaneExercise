using Assignment.Domain.Entities;
using Assignment.Domain.Interfaces.Repositories;

namespace Assignment.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<bool> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}
