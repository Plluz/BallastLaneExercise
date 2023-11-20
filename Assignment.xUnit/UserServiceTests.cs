using Assignment.Domain.Interfaces.Services;

namespace Assignment.xUnit;

public class UserServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _jwtService = Substitute.For<IJwtService>();
        _userService = new UserService(_userRepository, _jwtService);
    }

    [Theory]
    [InlineData("username", "validPassword1", "fakeToken1")]
    [InlineData("Bilbo Baggins", "validPassword2", "fakeToken2")]
    [InlineData("xXSuper__Gamer__99Xx", "validPassword3", "fakeToken3")]
    public async Task LoginAsync_ValidCredentials_ReturnsToken(string username, string password, string expectedToken)
    {
        // Arrange
        var user = new User { Username = username, Password = password };
        _userRepository.GetByUsernameAsync(username).Returns(user);
        _jwtService.GenerateToken(user).Returns(expectedToken);

        // Act
        var result = await _userService.LoginAsync(username, password);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task LoginAsync_InvalidCredentials_ReturnsEmptyString()
    {
        // Arrange
        const string username = "testUser";
        const string password = "password";
        _userRepository.GetByUsernameAsync(username).Returns((User)null);

        // Act
        var result = await _userService.LoginAsync(username, password);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task RegisterAsync_ValidRegistration_ReturnsTrue()
    {
        // Arrange
        const string username = "newUser";
        const string password = "password";
        const string passwordConfirm = "password";
        _userRepository.GetByUsernameAsync(username).Returns((User)null);
        _userRepository.AddAsync(Arg.Any<User>()).Returns(true);

        // Act
        var result = await _userService.RegisterAsync(username, password, passwordConfirm);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("user1", "password", "PASSWORD")]
    [InlineData("user2", "Different", "NotEqual")]
    [InlineData("user3", "12345678", "1234567")]
    public async Task RegisterAsync_PasswordMismatch_ReturnsFalse(string username, string password, string passwordConfirm)
    {
        // Act
        var result = await _userService.RegisterAsync(username, password, passwordConfirm);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RegisterAsync_ExistingUser_ReturnsFalse()
    {
        // Arrange
        const string username = "existingUser";
        const string password = "password";
        const string passwordConfirm = "password";
        _userRepository.GetByUsernameAsync(username).Returns(new User());

        // Act
        var result = await _userService.RegisterAsync(username, password, passwordConfirm);

        // Assert
        Assert.False(result);
    }
}
