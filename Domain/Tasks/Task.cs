using Domain.Projects;

namespace Domain.Task;

public class TaskDetail
{

    public TaskId Id { get; private set; }
    public ProjectId ProjectId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public DateTime DueDate { get; private set; }

    public byte Status { get; set; } = 1;
}
