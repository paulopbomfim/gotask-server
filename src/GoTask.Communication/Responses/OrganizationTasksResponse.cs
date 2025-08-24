using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record OrganizationTasksResponse()
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
    public TaskUser User { get; set; } = default!;
}

public record TaskUser
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public UserOrganization Organization { get; set; } = default!;
}

public record UserOrganization
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
}