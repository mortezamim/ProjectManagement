using Application.Data;
using Application.Project.Create;
using Domain.Projects;
using Domain.User;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid?>
{
    private readonly IUserRepository _UserRepository;
    private readonly IProjectRepository _ProjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IUserRepository UserRepository,
        IProjectRepository ProjectRepository,
        IUnitOfWork unitOfWork)
    {
        _UserRepository = UserRepository;
        _ProjectRepository = ProjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid?> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
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
