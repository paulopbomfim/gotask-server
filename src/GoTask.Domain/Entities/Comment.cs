namespace GoTask.Domain.Entities;

public record Comment
{
    public long Id { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public Guid CommentId { get; set; } = Guid.NewGuid();
    
    public Guid TaskId { get; set; }
    public TaskEntity Task { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}