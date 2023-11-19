using Assignment.Domain.Entities;
using Assignment.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Assignment.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = (configuration ?? throw new ArgumentNullException(nameof(configuration))).GetConnectionString("AssignmentConnection");
    }

    public async Task<bool> AddAsync(User user)
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string insertQuery = "INSERT INTO Users (Id, Username, Password) VALUES (@id, @username, @password)";

        using NpgsqlCommand command = new(insertQuery, connection);

        command.Parameters.AddWithValue("@id", Guid.NewGuid());
        command.Parameters.AddWithValue("@username", user.Username);
        command.Parameters.AddWithValue("@password", user.Password);

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected == 1;
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string query = "SELECT * FROM Users WHERE Id = @id";

        using NpgsqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@id", userId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapUser(reader);
        }

        return null;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string query = "SELECT * FROM Users WHERE Username = @username";

        using NpgsqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@username", username);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapUser(reader);
        }

        return null;
    }

    private static User MapUser(NpgsqlDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal(nameof(User.Id)));
        var username = reader.GetString(reader.GetOrdinal(nameof(User.Username)));
        var password = reader.GetString(reader.GetOrdinal(nameof(User.Password)));

        return new User(id, username, password);
    }
}
