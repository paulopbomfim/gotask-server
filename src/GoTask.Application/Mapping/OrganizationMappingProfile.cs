using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace GoTask.Application.Mapping;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class OrganizationMappingProfile
{
    [MapperIgnoreTarget(nameof(Organization.Id))]
    [MapperIgnoreTarget(nameof(Organization.CreatedAt))]
    [MapperIgnoreTarget(nameof(Organization.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Organization.Users))]
    public static partial Organization ToEntity(this OrganizationRequest request);
    
    public static partial OrganizationResponse ToResponse(this Organization organization);

    private static partial OrganizationUsersResponse ToOrganizationUsersResponse(this User user);
    
    [MapperIgnoreTarget(nameof(Organization.Id))]
    [MapperIgnoreTarget(nameof(Organization.Users))]
    [MapperIgnoreTarget(nameof(Organization.CreatedAt))]
    [MapperIgnoreTarget(nameof(Organization.UpdatedAt))]
    public static partial void ApplyUpdate([MappingTarget] this Organization organization, OrganizationRequest request);
    
    public static partial OrganizationTasksResponse ToOrganizationTasksResponse(this TaskEntity task);

    private static partial TaskUser ToTaskUser(this User user);
    
    public static partial UserOrganization ToUserOrganization(this Organization organization);
    
    
}