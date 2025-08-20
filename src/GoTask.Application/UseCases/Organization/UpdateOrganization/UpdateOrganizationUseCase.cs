using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Domain.Enums;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.UpdateOrganization;

public class UpdateOrganizationUseCase(
    IUserReadOnlyRepository userReadOnlyRepository,
    IOrganizationReadOnlyRepository organizationReadOnlyRepository,
    IOrganizationWriteOnlyRepository organizationWriteOnlyRepository,
    IUnitOfWork uow) : IUpdateOrganizationUseCase
{
    public async Task ExecuteAsync(
        long orgId,
        OrganizationRequest request,
        string userIdentification,
        CancellationToken ct)
    {
        Validate(request);
        
        var userId = Guid.Parse(userIdentification);
        var user = await userReadOnlyRepository
            .GetUserByIdentifierAsync(userId, cancellationToken: ct)
            ?? throw new NotFoundException();

        var organization = await organizationReadOnlyRepository
            .GetOrganizationByIdAsync(orgId, ct)
            ?? throw new NotFoundException();

        if (user.OrganizationId != organization.Id)
            throw new NotFoundException();
        
        if (user.Role != OrganizationRole.Admin)
            throw new UnauthorizedException();
        
        organization.ApplyUpdate(request);
        organization.UpdatedAt = DateTime.UtcNow;

        organizationWriteOnlyRepository.UpdateOrganization(organization);
        await uow.Commit(ct);
    }

    private static void Validate(
        OrganizationRequest request)
    {
        var validation = new OrganizationValidator().Validate(request);
        
        if(validation.IsValid) return;
        
        var errorMessages = validation.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}