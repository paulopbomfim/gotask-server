namespace GoTask.Application.UseCases.Organization.GetOrganization;

public class GetOrganizationUseCase : IGetOrganizationUseCase
{
    public Task ExecuteAsync(Guid userIdentifier, long organizationId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateAsync(Guid userIdentifier, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}