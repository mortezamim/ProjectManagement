using Domain.Projects;

namespace Domain.Task;

public class TaskDetail
{
    public TaskDetail(TaskId id, ProjectId projectId, string name, string description, DateTime dueDate, byte status)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = status;
    }

    public TaskId Id { get; private set; }
    public ProjectId ProjectId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

    public DateTime DueDate { get; private set; }

    public byte Status { get; private set; } = 1;
}
