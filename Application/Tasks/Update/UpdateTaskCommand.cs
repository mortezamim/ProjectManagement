using Domain.TaskDetails;
using MediatR;

namespace Application.TaskDetails.Update;

public record UpdateTaskCommand(TaskId TaskId, string Name, string Descriptions, byte Status) : IRequest;

public record UpdateTaskRequest(string Name, string Description, byte Status);
