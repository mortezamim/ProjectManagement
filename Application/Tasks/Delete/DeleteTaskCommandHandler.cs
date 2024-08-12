using Application.Data;
using Domain.Exceptions;
using Domain.Projects;
using Domain.TaskDetails;
using MediatR;

namespace Application.TaskDetails.Delete;

internal sealed class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IProjectRepository _ProjectRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(
        IProjectRepository ProjectRepository,
        IUnitOfWork unitOfWork,
        ITaskRepository taskRepository)
    {
        _ProjectRepository = ProjectRepository;
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId) ?? throw new TaskNotFoundException(request.TaskId);

        var project = await _ProjectRepository.GetByIdAsync(task.ProjectId) ?? throw new ProjectNotFoundException(task.ProjectId);

        if (project.UserId != request.UserId) throw new ProjectNotFoundException(task.ProjectId);

        _taskRepository.Remove(task);

        await _unitOfWork.SaveChangesAsync();

    }
}
