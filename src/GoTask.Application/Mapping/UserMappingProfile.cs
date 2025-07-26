using GoTask.Communication.Requests;
using Riok.Mapperly.Abstractions;
using GoTask.Domain.Entities;

namespace GoTask.Application.Mapping;

[Mapper(EnumNamingStrategy = EnumNamingStrategy.MemberName)]
public partial class UserMappingProfile
{
    public partial User ToEntity(UserRequest request);
}