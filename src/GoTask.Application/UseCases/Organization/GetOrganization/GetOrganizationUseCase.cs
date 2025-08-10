using GoTask.Application.Mapping;
using GoTask.Application.Services.User;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public class GetOrganizationUseCase(
    IUserContextService userContext,
    IUserReadOnlyRepository userRepository,
    IOrganizationReadOnlyRepository organizationRepository) : IGetOrganizationUseCase
{
    public async Task<OrganizationResponse> ExecuteAsync(long organizationId, CancellationToken ct)
    {
        var userId = userContext.UserIdentification;
        var userIdentification = Guid.Parse(userContext.UserIdentification);
        var loggedUser = await userRepository.GetUserByIdentifierAsync(userIdentification, cancellationToken: ct)
            ?? throw new UnauthorizedException();
        
        var organization = await organizationRepository.GetOrganizationByIdAsync(organizationId, ct)
            ?? throw new NotFoundException();

        if (loggedUser.OrganizationId != organization.Id)
        {
            throw new UnauthorizedException();
        }
        
        return organization.ToResponse();
    }
}