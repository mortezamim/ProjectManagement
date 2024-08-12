using Domain.Projects;
using MediatR;

namespace Application.TaskDetails.Create;

public record CreateTaskCommand(Guid UserId, ProjectId ProjectId, string Name, string Description, DateTime DueDate, byte Status) : IRequest<Guid?>;
public record CreateTaskRequest(string Name, string Description, DateTime DueDate, byte Status);
