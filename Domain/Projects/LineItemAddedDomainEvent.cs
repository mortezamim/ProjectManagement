using Domain.Primitives;
using Domain.Projects;
using Domain.Task;

namespace Domain.TaskDetails;

public sealed record TaskAddedDomainEvent(
    Guid Id,
    ProjectId ProjectId,
    TaskId TaskId) : DomainEvent(Id);
