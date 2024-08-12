using Domain.TaskDetails;
using MediatR;

namespace Application.TaskDetails.Update;

public record UpdateTaskCommand(Guid UserId, TaskId TaskId, string Name, string Descriptions, byte Status) : IRequest;

public record UpdateTaskRequest(string Name, string Description, byte Status);
