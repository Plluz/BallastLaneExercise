namespace Assignment.xUnit;

public class MeetingServiceTests
{
    [Fact]
    public async Task AddMeeting_ShouldAddMeetingToRepository()
    {
        // Arrange
        var meetingRepositorySubstitute = Substitute.For<IMeetingRepository>();
        var meetingService = new MeetingService(meetingRepositorySubstitute);

        var meeting = new Meeting();

        // Act
        await meetingService.AddAsync(meeting);

        // Assert
        await meetingRepositorySubstitute.Received(1).AddAsync(Arg.Any<Meeting>());
    }

    [Fact]
    public async Task GetMeetingById_ShouldReturnMeetingFromRepository()
    {
        // Arrange
        var meetingRepositorySubstitute = Substitute.For<IMeetingRepository>();
        var meetingService = new MeetingService(meetingRepositorySubstitute);

        var meetingId = Guid.NewGuid();
        meetingRepositorySubstitute.GetByIdAsync(meetingId).Returns(new Meeting());

        // Act
        var result = await meetingService.GetByIdAsync(meetingId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMeetings_ShouldReturnListOfMeetingsFromRepository()
    {
        // Arrange
        var meetingRepositorySubstitute = Substitute.For<IMeetingRepository>();
        var meetingService = new MeetingService(meetingRepositorySubstitute);

        var meetings = new List<Meeting>
        {
            new Meeting(),
            new Meeting(),
            new Meeting()
        };

        meetingRepositorySubstitute.GetAllAsync().Returns(meetings);

        // Act
        var result = await meetingService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(meetings.Count, result.Count());
    }

    [Fact]
    public async Task UpdateMeeting_ShouldUpdateMeetingInRepository()
    {
        // Arrange
        var meetingRepositorySubstitute = Substitute.For<IMeetingRepository>();
        var meetingService = new MeetingService(meetingRepositorySubstitute);

        var meetingId = Guid.NewGuid();
        var meeting = new Meeting();

        // Act
        await meetingService.UpdateAsync(meeting);

        // Assert
        await meetingRepositorySubstitute.Received(1).UpdateAsync( Arg.Any<Meeting>());
    }

    [Fact]
    public async Task DeleteMeeting_ShouldDeleteMeetingFromRepository()
    {
        // Arrange
        var meetingRepositorySubstitute = Substitute.For<IMeetingRepository>();
        var meetingService = new MeetingService(meetingRepositorySubstitute);

        var meetingId = Guid.NewGuid();

        // Act
        await meetingService.DeleteAsync(meetingId);

        // Assert
        await meetingRepositorySubstitute.Received(1).DeleteAsync(meetingId);
    }
}
