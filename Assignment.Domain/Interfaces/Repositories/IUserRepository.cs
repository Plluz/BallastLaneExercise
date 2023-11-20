namespace Assignment.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> AddAsync(User user);
    Task<User> GetByIdAsync(Guid userId);
    Task<User> GetByUsernameAsync(string username);
}