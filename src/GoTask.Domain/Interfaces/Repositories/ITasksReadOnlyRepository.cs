using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface ITasksReadOnlyRepository
{
    Task<IList<TaskEntity>> GetTasksAsync(long orgId, long? userId, CancellationToken cancellationToken = default);
    
    Task GetTaskByIdAsync();
}