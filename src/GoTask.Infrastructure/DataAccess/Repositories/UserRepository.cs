using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class UserRepository(GoTaskDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    #region Read
    
        public async Task<bool> ExistsActiveUserWithEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.AsNoTracking()
                .AnyAsync(user => user.Email.Equals(email), cancellationToken);
        }
        
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
        }

        public async Task<User?> GetUserByIdentifierAsync(Guid userIdentifier, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier, cancellationToken);
        }

    #endregion

    #region Write

        public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var result = await dbContext.Users.AddAsync(user, cancellationToken);
            return result.Entity;
        }

    #endregion
}