using Assignment.Domain.Entities;
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
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user is null || user.Password != password)
            return string.Empty;

        return JwtService.GenerateToken(user);
    }

    public async Task<bool> RegisterAsync(string username, string password, string passwordConfirm)
    {
        if (password != passwordConfirm)
            return false;

        var found = await _userRepository.GetByUsernameAsync(username);
        if (found is not null)
        {
            return false;
        }

        var user = new User
        {
            Username = username,
            Password = password
        };

        return await _userRepository.AddAsync(user);
    }
}
