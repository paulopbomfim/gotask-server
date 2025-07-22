using GoTask.Domain.Enums;

namespace GoTask.Domain.Entities;

public record User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    public OrganizationRole Role { get; set; } = OrganizationRole.MEMBER;
    
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}