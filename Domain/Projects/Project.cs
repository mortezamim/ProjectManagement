using Domain.Primitives;
using Domain.Task;
using Domain.TaskDetails;

namespace Domain.Projects;

public class Project : Entity
{
    public ProjectId Id { get; private set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; private set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    private readonly List<TaskDetail> _tasks = new();

    public IReadOnlyList<TaskDetail> Tasks => _tasks.ToList();

    public static Project Create(Guid userId, string name, string description)
    {
        var project = new Project
        {
            Id = new ProjectId(Guid.NewGuid()),
            Name = name,
            Description = description,
            UserId = userId
        };

        project.Raise(new ProjectCreatedDomainEvent(Guid.NewGuid(), project.Id));

        return project;
    }


    public void AddTaskItem(string name, string description, DateTime dueDate, byte status)
    {
        var taskItem = new TaskDetail(
            new TaskId(Guid.NewGuid()),
            this.Id,
            name,
            description,
            dueDate,
            status);

        _tasks.Add(taskItem);

        Raise(new TaskAddedDomainEvent(Guid.NewGuid(), Id, taskItem.Id));
    }

}
