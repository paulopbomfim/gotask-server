using GoTask.Application.Mapping;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public class GetOrganizationUseCase(
    IUserReadOnlyRepository userRepository,
    IOrganizationReadOnlyRepository organizationRepository) : IGetOrganizationUseCase
{
    public async Task<OrganizationResponse> ExecuteAsync(long organizationId, string userIdentification, CancellationToken ct)
    {
       
        var userGuid = Guid.Parse(userIdentification);
        var loggedUser = await userRepository.GetUserByIdentifierAsync(userGuid, cancellationToken: ct)
            ?? throw new NotFoundException();
        
        var organization = await organizationRepository.GetOrganizationWithUsersByIdAsync(organizationId, ct)
            ?? throw new NotFoundException();

        return loggedUser.OrganizationId == organization.Id 
            ? organization.ToResponse()
            : throw new UnauthorizedException();
    }
}