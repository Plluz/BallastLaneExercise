using Assignment.Domain.Interfaces.Repositories;

namespace Assignment.xUnit;

public class UserServiceTests
{
    [Fact]
    public async Task RegisterUser_ShouldAddUserToRepository()
    {
        // Arrange
        var userRepositorySubstitute = Substitute.For<IUserRepository>();
        var userService = new UserService(userRepositorySubstitute);

        var username = "some-name";
        var password = "some-password";
        var passwordConfirmation = "some-password";

        // Act
        await userService.RegisterAsync(username, password, passwordConfirmation);

        // Assert
        await userRepositorySubstitute.Received(1).AddAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUserFromRepository()
    {
        // Arrange
        var userRepositorySubstitute = Substitute.For<IUserRepository>();
        var userService = new UserService(userRepositorySubstitute);

        var userId = Guid.NewGuid();
        userRepositorySubstitute.GetByIdAsync(userId).Returns(new User());

        // Act
        var result = await userService.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task LoginUser_ShouldAuthenticateUser()
    {
        // Arrange
        var userRepositorySubstitute = Substitute.For<IUserRepository>();
        var userService = new UserService(userRepositorySubstitute);

        var username = "user";
        var password = "password";

        userRepositorySubstitute.GetByUsernameAsync(username).Returns(new User());

        // Act
        var result = await userService.LoginAsync(username, password);

        // Assert
        Assert.NotNull(result);
    }
}
