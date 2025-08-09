using GoTask.Application.Services.User;
using GoTask.Communication.Enums;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public class GetOrganizationUseCase(IUserContextService userContext, IUserReadOnlyRepository userRepository) : IGetOrganizationUseCase
{
    public Task ExecuteAsync(Guid userIdentifier, long organizationId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateAsync(Guid userIdentifier, CancellationToken ct)
    {
        var role = userContext.Role;

        if (role != nameof(OrganizationRole.Admin))
        {
            throw new UnauthorizedException();
        }
        
        
    }
}