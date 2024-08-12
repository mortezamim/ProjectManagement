using Application.Data;
using Application.Project.Delete;
using Domain.Projects;
using MediatR;

namespace Application.Orders.Create;

internal sealed class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _ProjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCommandHandler(
        IProjectRepository ProjectRepository,
        IUnitOfWork unitOfWork)
    {
        _ProjectRepository = ProjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {

        var project = await _ProjectRepository.GetByIdAsync(request.ProjectId);

        if (project == null)
        {
            throw new EntryPointNotFoundException();
        }

        _ProjectRepository.Delete(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
