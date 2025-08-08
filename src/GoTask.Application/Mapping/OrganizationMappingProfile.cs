using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace GoTask.Application.Mapping;

[Mapper]
public static partial class OrganizationMappingProfile
{
    [MapperIgnoreTarget(nameof(Organization.Id))]
    [MapperIgnoreTarget(nameof(Organization.CreatedAt))]
    [MapperIgnoreTarget(nameof(Organization.UpdatedAt))]
    [MapperIgnoreSource(nameof(request.UserIdentifier))]
    public static partial Organization ToEntity(this OrganizationRequest request);
    
    [MapperIgnoreSource(nameof(Organization.CreatedAt))]
    [MapperIgnoreSource(nameof(Organization.UpdatedAt))]
    public static partial OrganizationResponse ToResponse(this Organization organization);
}