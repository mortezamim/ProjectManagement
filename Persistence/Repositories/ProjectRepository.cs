using Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Project?> GetByIdAsync(ProjectId id)
    {
        return _context.Projects
            .Include(o => o.Tasks)
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Add(Project project)
    {
        _context.Projects.Add(project);
    }
}
