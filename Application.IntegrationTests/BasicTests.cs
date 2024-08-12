using Application.Exceptions;
using Application.Project.Create;
using Domain.User.Login;
using Domain.User.register;

namespace Application.IntegrationTests;

public class BasicTests : BaseIntegrationTest
{
    public BasicTests(IntegrationTestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task Register_ShouldThrowValidationException_WhenUsernameIsEmpty()
    {
        // Arrange
        var command = new RegisterUserCommand(string.Empty, "string.Empty", "Morteza", "Jafary");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task Register_ShouldThrowValidationException_WhenPasswordIsNull()
    {
        // Arrange
        var command = new RegisterUserCommand("u_test1", null, "Morteza", "Jafary");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task Register_ShouldThrowValidationException_WhenPasswordIsEmpty()
    {
        // Arrange
        var command = new RegisterUserCommand("u_test1", string.Empty, "Morteza", "Jafary");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task Create_ShouldRegister_WhenCommandIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("string", "string_password", "Morteza", "Jafary");

        // Act
        var userId = await Sender.Send(command);
        var user = DbContext.Users.FirstOrDefault(p => p.Id == userId);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public async Task Create_ShouldLogin_WhenCommandIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("stringg", "string_password", "Morteza", "Jafary");
        await Sender.Send(command);
        var query = new LoginUserCommand("stringg", "string_password");

        // Act
        var tokenResult = await Sender.Send(query);

        // Assert
        Assert.NotNull(tokenResult);
        Assert.NotEmpty(tokenResult.Token);
    }

    [Fact]
    public async Task Create_ShouldAddProject_WhenCommandIsValid()
    {
        // Arrange
        var userCommand = new RegisterUserCommand("User_11", "string.Empty", "Morteza", "Jafary");
        var userId = await Sender.Send(userCommand);

        var command = new CreateProjectCommand((Guid)userId, "prj_1", "Description__11");

        // Act
        var projectId = await Sender.Send(command);
        var project = DbContext.Projects.FirstOrDefault(p => p.Id == new Domain.Projects.ProjectId(projectId.Value));

        // Assert
        Assert.NotNull(project);
    }
}