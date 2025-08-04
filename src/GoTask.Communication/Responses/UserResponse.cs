using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record UserResponse
{
    public Guid UserIdentifier { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
    public long? OrganizationId { get; set; }
    public OrganizationRole Role { get; set; }
}