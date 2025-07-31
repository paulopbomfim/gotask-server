using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class UserRepository(GoTaskDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    #region Read
    
        public async Task<bool> ExistsActiveUserWithEmail(string email)
        {
            return await dbContext.Users.AsNoTracking()
                .AnyAsync(user => user.Email.Equals(email));
        }
        
        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

    #endregion

    #region Write

        public async Task<User> RegisterUser(User user)
        {
            var result = await dbContext.Users.AddAsync(user);
            return result.Entity;
        }

    #endregion
}