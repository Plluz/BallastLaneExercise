using Assignment.Domain.Interfaces.Repositories;

namespace Assignment.Domain.Services;

public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;

    public MeetingService(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
    }

    public async Task<bool> AddAsync(Meeting meeting)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Meeting>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Meeting> GetByIdAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid meetingId, Meeting meeting)
    {
        throw new NotImplementedException();
    }
}
