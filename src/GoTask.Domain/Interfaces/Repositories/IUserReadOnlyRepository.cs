using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
    
    public Task<User?> GetUserByEmail(string email);
}