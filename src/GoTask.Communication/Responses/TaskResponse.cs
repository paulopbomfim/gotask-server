using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record TaskResponse()
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
}