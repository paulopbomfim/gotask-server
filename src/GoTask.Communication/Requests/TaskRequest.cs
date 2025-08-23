using GoTask.Communication.Enums;

namespace GoTask.Communication.Requests;

public record TaskRequest()
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
}