using GoTask.Application.Mapping;
using GoTask.Application.Services.User;
using GoTask.Communication.Enums;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public class GetOrganizationUseCase(
    IUserContextService userContext,
    IUserReadOnlyRepository userRepository,
    IOrganizationReadOnlyRepository organizationRepository) : IGetOrganizationUseCase
{
    public async Task<OrganizationResponse> ExecuteAsync(Guid userIdentifier, long organizationId, CancellationToken ct)
    {
        var organization = await organizationRepository.GetOrganizationByIdAsync(organizationId, ct)
            ?? throw new NotFoundException();

        return organization.ToResponse();
    }

    private async Task ValidateAsync(Guid userIdentifier, CancellationToken ct)
    {
        var role = userContext.Role;
        var loggedUser = await userRepository.GetUserByIdentifierAsync(userIdentifier, cancellationToken: ct);

        if (role != nameof(OrganizationRole.Admin) || loggedUser is null)
        {
            throw new UnauthorizedException();
        }
    }
}