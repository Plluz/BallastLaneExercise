using Assignment.Domain.Entities;
using Assignment.Domain.Interfaces.Repositories;

namespace Assignment.Infrastructure.Repositories;

public class MeetingRepository : IMeetingRepository
{
    public async Task<bool> AddAsync(Meeting meeting)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Meeting>> GetAllAsync()
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
