using Application.Data;
using Application.Helpers;
using Domain.User;
using Domain.User.register;
using FluentValidation;
using MediatR;

namespace Application.Customers.Create;

internal sealed class RegisterUserRequestHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserRequestHandler(
        IUnitOfWork unitOfWork,
        IValidator<RegisterUserCommand> validator
,
        IUserRepository userRepository)
    {
        this.unitOfWork = unitOfWork;
        this.validator = validator;
        this.userRepository = userRepository;
    }

    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result.Errors);

        Utils.CreatePasswordHash(request.Password, out byte[] _hash, out byte[] _salt);

        var user = new User(Guid.NewGuid(), request.FirstName, request.LastName, request.Username, _hash, _salt);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }

}
