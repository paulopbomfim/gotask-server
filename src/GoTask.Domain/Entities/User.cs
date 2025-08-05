using GoTask.Domain.Enums;

namespace GoTask.Domain.Entities;

public record User
{
    public long Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public OrganizationRole Role { get; set; } = OrganizationRole.Member;
    
    public Guid UserIdentifier { get; init; } = Guid.NewGuid();
    
    public long? OrganizationId { get; set; }
    
    public Organization Organization { get; init; } = default!;
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}