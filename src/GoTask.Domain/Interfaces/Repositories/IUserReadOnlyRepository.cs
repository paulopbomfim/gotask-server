using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmailAsync(string email, CancellationToken cancellationToken = default);
    
    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    public Task<User?> GetUserByIdentifierAsync(Guid userIdentifier, bool useTracking = false, CancellationToken cancellationToken = default);
}