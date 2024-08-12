namespace Domain.TaskDetails;

public interface ITaskRepository
{
    Task<TaskDetail?> GetByIdAsync(TaskId id);

    void Add(TaskDetail task);

    void Update(TaskDetail task);

    void Remove(TaskDetail task);
}