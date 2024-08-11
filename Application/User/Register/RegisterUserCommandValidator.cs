using Domain.User;
using Domain.User.register;
using FluentValidation;

namespace Application.Customers.Create;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(5)
            .WithMessage("Password minimum length is 5 characters");

        RuleFor(x => x.Username).NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(5)
            .WithMessage("Username minimum length is 5 characters");

        RuleFor(c => c.Username).MustAsync(async (email, _) =>
        {
            return await userRepository.IsUsernameUniqueAsync(email);
        }).WithMessage("The username must be unique");
    }
}
