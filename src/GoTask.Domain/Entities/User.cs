using GoTask.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace GoTask.Domain.Entities;

public record User
{
    [MapperIgnore]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public OrganizationRole Role { get; set; } = OrganizationRole.Member;
    
    [MapperIgnore]
    public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    
    public long? OrganizationId { get; set; }
    
    [MapperIgnore]
    public Organization Organization { get; set; } = default!;
    
    [MapperIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [MapperIgnore]
    public DateTime? UpdatedAt { get; set; }
}