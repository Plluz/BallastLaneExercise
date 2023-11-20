using Assignment.Domain.Interfaces.Repositories;

namespace Assignment.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> LoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }
}
