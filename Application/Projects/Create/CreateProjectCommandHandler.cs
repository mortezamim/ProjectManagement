using Application.Data;
using Application.Project.Create;
using Domain.Projects;
using Domain.User;
using FluentValidation;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid?>
{
    private readonly IUserRepository _UserRepository;
    private readonly IProjectRepository _ProjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProjectCommand> validator;

    public CreateProjectCommandHandler(
        IUserRepository UserRepository,
        IProjectRepository ProjectRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProjectCommand> validator)
    {
        _UserRepository = UserRepository;
        _ProjectRepository = ProjectRepository;
        _unitOfWork = unitOfWork;
        this.validator = validator;
    }

    public async Task<Guid?> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result.Errors);

        if (await _UserRepository.GetByIdAsync(request.UserId) is null)
        {
            return null;
        }

        var project = Domain.Projects.Project.Create(request.UserId, request.Name, request.Description);

        _ProjectRepository.Add(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id.Value;
    }
}
