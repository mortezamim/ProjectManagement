using Application.Data;
using Application.Helpers;
using Domain.User;
using Domain.User.Login;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Customers.Create;

internal sealed class LoginUserRequestHandler : IRequestHandler<LoginUserCommand, LoginUserResponse?>
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public LoginUserRequestHandler(
        IUnitOfWork unitOfWork,

        IUserRepository userRepository,
        IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public async Task<LoginUserResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await userRepository.GetByUsernameAsync(request.Username);

        if (userFromDb == null) return null;

        if (!Utils.VerifyPasswordHash(request.Password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
            return null;

        var token = Utils.GenerateJwtToken(userFromDb, DateTime.Now.AddHours(3), configuration["HmacSecretKey"]);

        return new LoginUserResponse(token);
    }
}
