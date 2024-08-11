using Domain.Primitives;
using Domain.Task;

namespace Domain.Projects;

public class Project : Entity
{
    public ProjectId Id { get; private set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; private set; }

    private readonly List<TaskDetail> _tasks = new();

    public IReadOnlyList<TaskDetail> Tasks => _tasks.ToList();

    public static Project Create(Guid userId)
    {
        var project = new Project
        {
            Id = new ProjectId(Guid.NewGuid()),
            UserId = userId
        };

        project.Raise(new ProjectCreatedDomainEvent(Guid.NewGuid(), project.Id));

        return project;
    }
}
