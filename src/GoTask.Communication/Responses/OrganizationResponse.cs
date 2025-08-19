using GoTask.Communication.Enums;

namespace GoTask.Communication.Responses;

public record OrganizationResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;

    public IEnumerable<OrganizationUsersResponse> Users { get; set; } = [];
}

public record OrganizationUsersResponse
{
    public Guid UserIdentifier { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public OrganizationRole Role { get; set; }
    public string ImageReference { get; set; } = string.Empty;
}