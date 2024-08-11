namespace Domain.Projects;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(ProjectId id);

    void Add(Project project);
}