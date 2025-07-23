using GoTask.Domain.Interfaces.Repositories;

namespace GoTask.Infrastructure.DataAccess;

public class UnitOfWork(GoTaskDbContext dbContext) : IUnitOfWork
{
    public async Task Commit() => await dbContext.SaveChangesAsync();
}