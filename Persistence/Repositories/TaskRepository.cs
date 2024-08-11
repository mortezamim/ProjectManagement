using Domain.Products;
using Domain.Task;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<TaskDetail?> GetByIdAsync(TaskId id)
    {
        return _context.Tasks
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public void Add(TaskDetail task)
    {
        _context.Tasks.Add(task);
    }

    public void Update(TaskDetail task)
    {
        _context.Tasks.Update(task);
    }

    public void Remove(TaskDetail task)
    {
        _context.Tasks.Remove(task);
    }

}
