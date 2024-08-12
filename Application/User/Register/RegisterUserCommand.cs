using Application.Abstractions.Messaging;

namespace Domain.User.register;

public record RegisterUserCommand(string Username, string Password, string FirstName, string LastName) : ICommand<Guid?>;
public record RegisterUserRequest(string Username, string Password, string FirstName, string LastName);
