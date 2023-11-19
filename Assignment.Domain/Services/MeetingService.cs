using Assignment.Domain.Entities;
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
        if (meeting.StartDate > meeting.EndDate)
        {
            return false;
        }

        return await _meetingRepository.AddAsync(meeting);
    }

    public async Task<bool> DeleteAsync(Guid meetingId)
    {
        var found = await _meetingRepository.GetByIdAsync(meetingId);
        if (found is null)
        {
            return false;
        }

        return await _meetingRepository.DeleteAsync(meetingId);
    }

    public async Task<IEnumerable<Meeting>> GetAllAsync()
    {
        return await _meetingRepository.GetAllAsync();
    }

    public async Task<Meeting> GetByIdAsync(Guid meetingId)
    {
        return await _meetingRepository.GetByIdAsync(meetingId);
    }

    public async Task<bool> UpdateAsync(Meeting meeting)
    {
        if (meeting.StartDate > meeting.EndDate)
        {
            return false;
        }

        var found = await _meetingRepository.GetByIdAsync(meeting.Id);
        if (found is null)
        {
            return false;
        }

        return await _meetingRepository.UpdateAsync(meeting);
    }
}
