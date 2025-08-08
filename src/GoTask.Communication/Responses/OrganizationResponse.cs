namespace GoTask.Communication.Responses;

public record OrganizationResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
}