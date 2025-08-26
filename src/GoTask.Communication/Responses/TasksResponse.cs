using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record TasksResponse()
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
    public TaskUser User { get; set; } = default!;
}

public record TasksUser
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public UsersOrganization Organization { get; set; } = default!;
}

public record UsersOrganization
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
}