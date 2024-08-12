using Domain.TaskDetails;

namespace Domain.Exceptions;

public sealed class TaskNotFoundException : Exception
{
    public TaskNotFoundException(TaskId id)
        : base($"The task with the ID = {id.Value} was not found")
    {
    }
}
