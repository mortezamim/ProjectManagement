using Application.Abstractions.Messaging;

namespace Domain.User.Login;

public record LoginUserCommand(string Username, string Password) : ICommand<LoginUserResponse?>;
public record LoginUserRequest(string Username, string Password);
public record LoginUserResponse(string Token);
