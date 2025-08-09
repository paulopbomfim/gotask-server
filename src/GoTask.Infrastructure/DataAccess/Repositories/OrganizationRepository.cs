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
    
    public async Task<Organization?> GetOrganizationByIdAsync(long organizationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Organizations
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == organizationId, cancellationToken);
    }
}