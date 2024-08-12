using Application.Data;
using Application.TaskDetails.Create;
using Domain.Exceptions;
using Domain.Projects;
using Domain.User;
using FluentValidation;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid?>
{
    private readonly IUserRepository _UserRepository;
    private readonly IProjectRepository _ProjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateTaskCommand> validator;

    public CreateTaskCommandHandler(
        IUserRepository UserRepository,
        IProjectRepository ProjectRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateTaskCommand> validator)
    {
        _UserRepository = UserRepository;
        _ProjectRepository = ProjectRepository;
        _unitOfWork = unitOfWork;
        this.validator = validator;
    }

    public async Task<Guid?> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result.Errors);

        var project = await _ProjectRepository.GetByIdAsync(request.ProjectId);
        if (project is null)
        {
            throw new ProjectNotFoundException(request.ProjectId);
        }

        if (project.UserId != request.UserId)
        {
            throw new ProjectNotFoundException(request.ProjectId);
        }

        project.AddTaskItem(request.Name, request.Description, request.DueDate, request.Status);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id.Value;
    }
}
