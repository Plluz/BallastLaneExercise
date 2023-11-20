namespace Assignment.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByIdAsync(Guid userId);
    Task<User> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(User user);
}
