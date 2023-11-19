namespace Assignment.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByIdAsync(Guid userId);
    Task<string> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(string username, string password, string passwordConfirm);
}
