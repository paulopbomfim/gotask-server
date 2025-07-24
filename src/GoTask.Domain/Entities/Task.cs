using GoTask.Domain.Enums;

namespace GoTask.Domain.Entities;

public record TaskEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
    
    public long UserId { get; set; }
    public User User { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}