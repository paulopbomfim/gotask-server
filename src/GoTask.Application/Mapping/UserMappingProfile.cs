using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Riok.Mapperly.Abstractions;
using GoTask.Domain.Entities;

namespace GoTask.Application.Mapping;

[Mapper(EnumNamingStrategy = EnumNamingStrategy.MemberName)]
public static partial class UserMappingProfile
{
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.UserIdentifier))]
    [MapperIgnoreTarget(nameof(User.Organization))]
    [MapperIgnoreTarget(nameof(User.CreatedAt))]
    [MapperIgnoreTarget(nameof(User.UpdatedAt))]
    public static partial User ToEntity(this UserRequest request);
    
    
    [MapperIgnoreSource(nameof(User.Id))]
    [MapperIgnoreSource(nameof(User.Organization))]
    [MapperIgnoreSource(nameof(User.CreatedAt))]
    [MapperIgnoreSource(nameof(User.UpdatedAt))]
    [MapperIgnoreSource(nameof(User.Password))]
    public static partial UserResponse ToResponse(this User user);
}