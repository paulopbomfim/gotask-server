using GoTask.Domain.Interfaces.Repositories;

namespace GoTask.Infrastructure.DataAccess;

public class UnitOfWork(GoTaskDbContext dbContext) : IUnitOfWork
{
    public async Task<int> Commit(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}