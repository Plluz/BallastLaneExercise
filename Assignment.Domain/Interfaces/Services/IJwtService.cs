namespace Assignment.Domain.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
