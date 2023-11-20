namespace Assignment.Domain.Interfaces.Services;

public interface IMeetingService
{
    Task<bool> AddAsync(Meeting meeting);
    Task<bool> DeleteAsync(Guid meetingId);
    Task<List<Meeting>> GetAllAsync();
    Task<Meeting> GetByIdAsync(Guid meetingId);
    Task<bool> UpdateAsync(Guid meetingId, Meeting meeting);
}
