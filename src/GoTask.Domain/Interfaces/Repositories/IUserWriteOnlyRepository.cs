using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface IUserWriteOnlyRepository
{
    Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default);
}