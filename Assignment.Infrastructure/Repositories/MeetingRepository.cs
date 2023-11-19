using Assignment.Domain.Entities;
using Assignment.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Assignment.Infrastructure.Repositories;

public class MeetingRepository : IMeetingRepository
{
    private readonly string _connectionString;

    public MeetingRepository(IConfiguration configuration)
    {
        _connectionString = (configuration ?? throw new ArgumentNullException(nameof(configuration))).GetConnectionString("AssignmentConnection");
    }

    public async Task<bool> AddAsync(Meeting meeting)
    {
        using NpgsqlConnection connection = new(_connectionString);
        
        await connection.OpenAsync();

        string insertQuery = "INSERT INTO Meetings (Id, Title, StartDate, EndDate) VALUES (@id, @title, @startDate, @endDate)";

        using NpgsqlCommand command = new(insertQuery, connection);
        
        command.Parameters.AddWithValue("@id", Guid.NewGuid());
        command.Parameters.AddWithValue("@title", meeting.Title);
        command.Parameters.AddWithValue("@startDate", meeting.StartDate);
        command.Parameters.AddWithValue("@endDate", meeting.EndDate);

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected == 1;
    }

    public async Task<bool> DeleteAsync(Guid meetingId)
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string query = "DELETE FROM Meetings WHERE Id = @MeetingId";

        using NpgsqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@MeetingId", meetingId);

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected == 1;
    }

    public async Task<IEnumerable<Meeting>> GetAllAsync()
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string selectQuery = "SELECT * FROM Meetings ORDER BY StartDate";

        using NpgsqlCommand command = new(selectQuery, connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var meetings = new List<Meeting>();

        while (await reader.ReadAsync())
        {
            meetings.Add(MapMeeting(reader));
        }

        return meetings;
    }

    public async Task<Meeting> GetByIdAsync(Guid meetingId)
    {
        using NpgsqlConnection connection = new(_connectionString);
        
        await connection.OpenAsync();

        string query = "SELECT * FROM Meetings WHERE Id = @MeetingId";

        using NpgsqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@MeetingId", meetingId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            return MapMeeting(reader);
        }

        return null;
    }

    public async Task<bool> UpdateAsync(Meeting meeting)
    {
        using NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        string updateQuery = @"UPDATE Meetings
            SET Title = @title, StartDate = @startDate, EndDate = @endDate
            WHERE Id = @meetingId";

        using NpgsqlCommand command = new(updateQuery, connection);

        command.Parameters.AddWithValue("@meetingId", meeting.Id);
        command.Parameters.AddWithValue("@title", meeting.Title);
        command.Parameters.AddWithValue("@startDate", meeting.StartDate);
        command.Parameters.AddWithValue("@endDate", meeting.EndDate);

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected == 1;
    }

    private static Meeting MapMeeting(NpgsqlDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal(nameof(Meeting.Id)));
        var title = reader.GetString(reader.GetOrdinal(nameof(Meeting.Title)));
        var startDate = reader.GetDateTime(reader.GetOrdinal(nameof(Meeting.StartDate)));
        var endDate = reader.GetDateTime(reader.GetOrdinal(nameof(Meeting.EndDate)));

        return new Meeting(id, title, startDate, endDate);
    }
}
