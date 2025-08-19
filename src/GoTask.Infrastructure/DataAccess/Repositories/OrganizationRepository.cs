using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess.Repositories;

public class OrganizationRepository(GoTaskDbContext dbContext) : IOrganizationWriteOnlyRepository, IOrganizationReadOnlyRepository
{
    public async Task<Organization> RegisterOrganizationAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Organizations.AddAsync(organization, cancellationToken);
        
        return result.Entity;
    }
    
    public async Task<Organization?> GetOrganizationWithUsersByIdAsync(long organizationId, CancellationToken cancellationToken = default)
    {
        var organizationQuery = dbContext.Organizations
            .AsNoTracking()
            .AsSplitQuery()
            .Include(o => o.Users);

        return await organizationQuery
            .SingleOrDefaultAsync(o => o.Id == organizationId, cancellationToken);

    }
}