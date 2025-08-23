using GoTask.Domain.Enums;

namespace GoTask.Domain.Entities;

public record TaskEntity
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
    
    public long UserId { get; set; }
    public User User { get; init; } = default!;
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}