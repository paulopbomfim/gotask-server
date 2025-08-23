using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface ITasksWriteOnlyRepository
{
    Task<TaskEntity> RegisterTaskAsync(TaskEntity task, CancellationToken cancellationToken = default);
    void UpdateTask();
    void DeleteTask();
}