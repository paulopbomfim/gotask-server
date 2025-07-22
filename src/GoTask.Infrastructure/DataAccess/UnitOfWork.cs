using GoTask.Domain.Interfaces.Repositories;

namespace GoTask.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    public Task Commit()
    {
        throw new NotImplementedException();
    }
}