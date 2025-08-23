using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class TasksRepository(GoTaskDbContext dbContext) : ITasksReadOnlyRepository, ITasksWriteOnlyRepository
{
    #region Read
    
    public Task GetTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetTaskByIdAsync()
    {
        throw new NotImplementedException();
    }
    
    #endregion
    
    #region Write
    
    public async Task<TaskEntity> RegisterTaskAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        var newTask = await dbContext.Tasks.AddAsync(task, cancellationToken);
        return newTask.Entity;
    }

    public void UpdateTask()
    {
        throw new NotImplementedException();
    }

    public void DeleteTask()
    {
        throw new NotImplementedException();
    }
    
    #endregion
}