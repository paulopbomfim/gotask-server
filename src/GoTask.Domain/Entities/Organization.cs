namespace GoTask.Domain.Entities;

public record Organization
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    
    public List<User> Users { get; set; } = [];
}