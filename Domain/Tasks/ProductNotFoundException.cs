namespace Domain.TaskDetails;

public sealed class TaskNotFoundException : Exception
{
    public TaskNotFoundException(TaskId id)
        : base($"The task with the ID = {id.Value} was not found")
    {
    }
}
