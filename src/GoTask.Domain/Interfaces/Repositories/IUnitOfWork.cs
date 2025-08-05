namespace GoTask.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken ct = default);
}