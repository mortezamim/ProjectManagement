using MediatR;

namespace Application.Project.Create;

public record CreateProjectCommand(Guid UserId, string Name, string Description) : IRequest<Guid?>;
public record CreateProjectRequest(string Name, string Description);
