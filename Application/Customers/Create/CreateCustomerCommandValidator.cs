using Domain.Customers;
using FluentValidation;

namespace Application.Customers.Create;

public sealed class RegisterUserCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public RegisterUserCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is not in a valid format.");

        RuleFor(c => c.Email).MustAsync(async (email, _) =>
        {
            return await customerRepository.IsEmailUniqueAsync(email);
        }).WithMessage("The email must be unique");
    }
}
