using Application.Data;
using Domain.Exceptions;
using Domain.Projects;
using Domain.TaskDetails;
using FluentValidation;
using MediatR;

namespace Application.TaskDetails.Update;

internal sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateTaskCommand> validator;

    public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IProjectRepository projectRepository, IValidator<UpdateTaskCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        this.validator = validator;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result.Errors);

        var task = await _taskRepository.GetByIdAsync(request.TaskId) ?? throw new TaskNotFoundException(request.TaskId);

        var project = await _projectRepository.GetByIdAsync(task.ProjectId) ?? throw new ProjectNotFoundException(task.ProjectId);

        if (project.UserId != request.UserId)
        {
            throw new ProjectNotFoundException(task.ProjectId);
        }

        task.Update(request.Name, request.Descriptions, request.Status);

        _taskRepository.Update(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
