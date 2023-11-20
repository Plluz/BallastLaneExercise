namespace Assignment.Domain.Interfaces.Repositories;

public interface IMeetingRepository
{
    Task<bool> AddAsync(Meeting meeting);
    Task<bool> DeleteAsync(Guid meetingId);
    Task<List<Meeting>> GetAllAsync();
    Task<Meeting> GetByIdAsync(Guid meetingId);
    Task<bool> UpdateAsync(Meeting meeting);
}