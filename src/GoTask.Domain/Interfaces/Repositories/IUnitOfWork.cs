namespace GoTask.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}