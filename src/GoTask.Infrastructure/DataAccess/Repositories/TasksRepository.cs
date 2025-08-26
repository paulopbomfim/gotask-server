using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class TasksRepository(GoTaskDbContext dbContext) : ITasksReadOnlyRepository, ITasksWriteOnlyRepository
{
    #region Read
    
    public async Task<IList<TaskEntity>> GetTasksAsync(long orgId, long? userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Tasks
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.User)
            .ThenInclude(x => x.Organization)
            .Where(p => 
                p.User.Organization.Id == orgId && (userId == null || userId == p.User.Id))
            .ToListAsync(cancellationToken);
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