using Domain.TaskDetails;
using MediatR;

namespace Application.TaskDetails.Delete;

public record DeleteTaskCommand(Guid UserId, TaskId TaskId) : IRequest;
