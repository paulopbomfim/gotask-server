using System.ComponentModel.DataAnnotations;
using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Enums;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.Register;

public class RegisterOrganizationUseCase(
    IOrganizationWriteOnlyRepository organizationWriteOnlyRepository,
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork uow) : IRegisterOrganizationUseCase
{
    public async Task<(long orgId, OrganizationResponse organizationInfo)> ExecuteAsync(OrganizationRequest request, CancellationToken ct)
    {
        Validator(request);

        var newUserAdmin = await userReadOnlyRepository
            .GetUserByIdentifierAsync(request.UserIdentifier, cancellationToken: ct)
                ?? throw new NotFoundException();

        var organizationMap = request.ToEntity();
        var newOrganization = await organizationWriteOnlyRepository
            .RegisterOrganizationAsync(organizationMap, cancellationToken: ct);
        
        await uow.Commit(ct);
        
        newUserAdmin.OrganizationId = newOrganization.Id;
        newUserAdmin.Role = OrganizationRole.Admin;
        newUserAdmin.UpdatedAt = DateTime.UtcNow;
        
        userWriteOnlyRepository.UpdateUser(newUserAdmin);
        await uow.Commit(ct);
        
        return (newOrganization.Id, newOrganization.ToResponse());
    }

    private static void Validator(OrganizationRequest request)
    {
        var validation = new RegisterOrganizationValidator().Validate(request);
        
        if(validation.IsValid) return;
        
        var errorMessages = validation.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}