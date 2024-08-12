using Domain.Projects;
using MediatR;

namespace Application.Project.Delete;

public record DeleteProjectCommand(Guid UserId, ProjectId ProjectId) : IRequest;
