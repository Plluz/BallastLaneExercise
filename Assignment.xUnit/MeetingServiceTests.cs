namespace Assignment.xUnit;

public class MeetingServiceTests
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly MeetingService _meetingService;

    public MeetingServiceTests()
    {
        _meetingRepository = Substitute.For<IMeetingRepository>();
        _meetingService = new MeetingService(_meetingRepository);
    }

    [Fact]
    public async Task AddAsync_ValidMeeting_ReturnsTrue()
    {
        // Arrange
        var validMeeting = new Meeting { Title = "title here", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) };

        _meetingRepository.AddAsync(validMeeting).Returns(true);

        // Act
        var result = await _meetingService.AddAsync(validMeeting);

        // Assert
        await _meetingRepository.Received(1).AddAsync(Arg.Any<Meeting>());
        Assert.True(result);
    }

    [Theory]
    [InlineData("2023-11-20T12:00:00", "2023-11-19T23:00:00")]
    [InlineData("2023-11-20T13:00:00", "2023-11-20T12:59:59")]
    public async Task AddAsync_NegativeHours_ReturnsFalse(string startDate, string endDate)
    {
        // Arrange
        var invalidMeeting = new Meeting
        {
            StartDate = DateTime.Parse(startDate),
            EndDate = DateTime.Parse(endDate)
        };


        // Act
        var result = await _meetingService.AddAsync(invalidMeeting);

        // Assert
        Assert.False(result);
        await _meetingRepository.DidNotReceive().AddAsync(Arg.Any<Meeting>());
    }

    [Fact]
    public async Task DeleteAsync_ExistingMeetingId_ReturnsTrue()
    {
        // Arrange
        var existingMeetingId = Guid.NewGuid();
        _meetingRepository.GetByIdAsync(existingMeetingId).Returns(new Meeting());
        _meetingRepository.DeleteAsync(existingMeetingId).Returns(true);

        // Act
        var result = await _meetingService.DeleteAsync(existingMeetingId);

        // Assert
        await _meetingRepository.Received(1).DeleteAsync(existingMeetingId);
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingMeetingId_ReturnsFalse()
    {
        // Arrange
        var nonExistingMeetingId = Guid.NewGuid();
        _meetingRepository.GetByIdAsync(nonExistingMeetingId).Returns((Meeting)null);

        // Act
        var result = await _meetingService.DeleteAsync(nonExistingMeetingId);

        // Assert
        Assert.False(result);
        await _meetingRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfMeetings()
    {
        // Arrange
        var meetings = new List<Meeting> { new(), new() };
        _meetingRepository.GetAllAsync().Returns(meetings);

        // Act
        var result = await _meetingService.GetAllAsync();

        // Assert
        Assert.Equal(meetings.Count, result.Count());
        Assert.Equal(meetings, result);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingMeetingId_ReturnsMeeting()
    {
        // Arrange
        var existingMeetingId = Guid.NewGuid();
        var existingMeeting = new Meeting { Id = existingMeetingId };
        _meetingRepository.GetByIdAsync(existingMeetingId).Returns(existingMeeting);

        // Act
        var result = await _meetingService.GetByIdAsync(existingMeetingId);

        // Assert
        Assert.Equal(existingMeeting, result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingMeetingId_ReturnsNull()
    {
        // Arrange
        var nonExistingMeetingId = Guid.NewGuid();
        _meetingRepository.GetByIdAsync(nonExistingMeetingId).Returns((Meeting)null);

        // Act
        var result = await _meetingService.GetByIdAsync(nonExistingMeetingId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ExistingMeeting_ReturnsTrue()
    {
        // Arrange
        var existingMeetingId = Guid.NewGuid();
        var existingMeeting = new Meeting { Id = existingMeetingId, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };
        _meetingRepository.GetByIdAsync(existingMeetingId).Returns(existingMeeting);

        var updatedMeeting = new Meeting
        {
            Id = existingMeetingId,
            StartDate = existingMeeting.StartDate.AddDays(1),
            EndDate = existingMeeting.EndDate.AddDays(1)
        };

        _meetingRepository.UpdateAsync(Arg.Any<Meeting>()).Returns(true);

        // Act
        var result = await _meetingService.UpdateAsync(updatedMeeting);

        // Assert
        await _meetingRepository.Received(1).UpdateAsync(Arg.Any<Meeting>());
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingMeeting_ReturnsFalse()
    {
        // Arrange
        var nonExistingMeeting = new Meeting { Id = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };
        _meetingRepository.GetByIdAsync(nonExistingMeeting.Id).Returns((Meeting)null);

        // Act
        var result = await _meetingService.UpdateAsync(nonExistingMeeting);

        // Assert
        Assert.False(result);
        await _meetingRepository.DidNotReceive().UpdateAsync(Arg.Any<Meeting>());
    }
}
