namespace GoTask.Domain.Interfaces.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
}