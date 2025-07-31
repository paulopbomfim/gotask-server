using GoTask.Communication.Requests;
using Riok.Mapperly.Abstractions;
using GoTask.Domain.Entities;

namespace GoTask.Application.Mapping;

[Mapper(EnumNamingStrategy = EnumNamingStrategy.MemberName)]
public static partial class UserMappingProfile
{
    public static partial User ToEntity(this UserRequest request);
}