using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record TaskWithUserResponse()
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
    public TaskUserResponse User { get; set; } = default!;
}

public record TaskUserResponse
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
}