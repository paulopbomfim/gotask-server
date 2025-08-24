using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class OrganizationRepository(GoTaskDbContext dbContext) : IOrganizationWriteOnlyRepository, IOrganizationReadOnlyRepository
{

    #region Read
    public async Task<Organization?> GetOrganizationWithUsersByIdAsync(long organizationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Organizations
            .AsNoTracking()
            .AsSplitQuery()
            .Include(o => o.Users)
            .SingleOrDefaultAsync(o => o.Id == organizationId, cancellationToken);
    }

    public async Task<Organization?> GetOrganizationByIdAsync(long organizationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Organizations
            .SingleOrDefaultAsync(o => o.Id == organizationId, cancellationToken);
    }

    public async Task<Organization?> GetOrganizationTasksAsync(
        long organizationId,
        IList<long>? usersId,
        CancellationToken cancellationToken = default)
    {
        var hasUserFilter = usersId is { Count: > 0 };

        var query = dbContext.Organizations
            .AsNoTracking()
            .AsSplitQuery()
            .Where(o => o.Id == organizationId);

        query = hasUserFilter
            ? query
                .Include(o => o.Users.Where(u => usersId!.Contains(u.Id)))
                .ThenInclude(u => u.Tasks)
            
            : query
                .Include(o => o.Users)
                .ThenInclude(u => u.Tasks);

        return await query.SingleOrDefaultAsync(cancellationToken);

    }
    
    #endregion
    
    #region Write
    public async Task<Organization> RegisterOrganizationAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Organizations.AddAsync(organization, cancellationToken);
        
        return result.Entity;
    }
    
    public void UpdateOrganization(Organization organizationToUpdate)
    {
        dbContext.Organizations.Update(organizationToUpdate);
    }
    #endregion
}