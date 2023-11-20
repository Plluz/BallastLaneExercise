using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Assignment.Infrastructure.Data;

public class DatabaseSeeder : IHostedService
{
    private readonly string _connectionString;

    public DatabaseSeeder(IOptions<DatabaseOptions> databaseOptions)
    {
        _connectionString = databaseOptions.Value.AssignmentConnection;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await WaitForDatabaseAsync();
        await SeedAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task SeedAsync()
    {
        using NpgsqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await CreateTableUsersAsync(connection);
        await CreateTableMeetingsAsync(connection);
        await SeedUsersAsync(connection);
        await SeedMeetingsAsync(connection);

        await connection.CloseAsync();
    }

    private async Task CreateTableUsersAsync(NpgsqlConnection connection)
    {
        using NpgsqlCommand command = new(
            "CREATE TABLE IF NOT EXISTS Users (" +
            "Id UUID PRIMARY KEY," +
            "Username VARCHAR(64) NOT NULL," +
            "Password VARCHAR(64) NOT NULL);", connection);

        await command.ExecuteNonQueryAsync();
    }

    private async Task CreateTableMeetingsAsync(NpgsqlConnection connection)
    {
        using NpgsqlCommand command = new(
            "CREATE TABLE IF NOT EXISTS Meetings (" +
            "Id UUID PRIMARY KEY," +
            "Title VARCHAR(128) NOT NULL," +
            "StartDate TIMESTAMP NOT NULL," +
            "EndDate TIMESTAMP NOT NULL);", connection);

        await command.ExecuteNonQueryAsync();
    }

    private async Task SeedUsersAsync(NpgsqlConnection connection)
    {
        using NpgsqlCommand selectQuery = new("SELECT COUNT(*) FROM Users", connection);
        int rowCount = Convert.ToInt32(await selectQuery.ExecuteScalarAsync());

        if (rowCount != 0) return;

        using NpgsqlCommand command = new(
            "INSERT INTO Users (Id, Username, Password) VALUES " +
            "(gen_random_uuid(), 'admin', 'password');", connection);

        await command.ExecuteNonQueryAsync();
    }

    private async Task SeedMeetingsAsync(NpgsqlConnection connection)
    {
        using NpgsqlCommand selectQuery = new("SELECT COUNT(*) FROM Meetings", connection);
        int rowCount = Convert.ToInt32(await selectQuery.ExecuteScalarAsync());

        if (rowCount != 0) return;

        using NpgsqlCommand command = new(
            "INSERT INTO Meetings (Id, Title, StartDate, EndDate) VALUES " +
            "(gen_random_uuid(), 'Test meeting 1 (seed)', '2023-11-20T11:00:00', '2023-11-20T11:15:00')," +
            "(gen_random_uuid(), 'Test meeting 2 (seed)', '2023-11-19T13:30:00', '2023-11-19T15:00:00')," +
            "(gen_random_uuid(), 'Test meeting 3 (seed)', '2023-11-20T16:00:00', '2023-11-20T17:30:00')," +
            "(gen_random_uuid(), 'Test meeting 4 (seed)', '2023-11-19T19:00:00', '2023-11-19T19:15:00')," +
            "(gen_random_uuid(), 'Test meeting 5 (seed)', '2023-11-20T19:15:00', '2023-11-20T19:30:00')," +
            "(gen_random_uuid(), 'Test meeting 6 (seed)', '2023-11-20T19:45:00', '2023-11-20T20:45:00');", connection);

        await command.ExecuteNonQueryAsync();
    }

    private async Task WaitForDatabaseAsync()
    {
        const int maxRetryAttempts = 10;
        const int delayMilliseconds = 2000;

        for (int attempt = 1; attempt <= maxRetryAttempts; attempt++)
        {
            try
            {
                using NpgsqlConnection connection = new(_connectionString);
                await connection.OpenAsync();
                await connection.CloseAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {attempt}/{maxRetryAttempts}: {ex.Message}");
            }

            await Task.Delay(delayMilliseconds);
        }

        throw new InvalidOperationException("Unable to connect to the database after multiple attempts.");
    }
}