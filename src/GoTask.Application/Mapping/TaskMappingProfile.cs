using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace GoTask.Application.Mapping;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target, EnumNamingStrategy = EnumNamingStrategy.MemberName)]
public static partial class TaskMappingProfile
{
    [MapperIgnoreTarget(nameof(TaskEntity.Id))]
    [MapperIgnoreTarget(nameof(TaskEntity.UserId))]
    [MapperIgnoreTarget(nameof(TaskEntity.User))]
    [MapperIgnoreTarget(nameof(TaskEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(TaskEntity.UpdatedAt))]
    public static partial TaskEntity ToEntity(this TaskRequest request);
    
    public static partial TaskResponse ToResponse(this TaskEntity task);
    
    private static partial TaskUserResponse ToTaskUserResponse(this User user);
}      