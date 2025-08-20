namespace GoTask.Communication.Requests;

public record OrganizationRequest()
{
    public string Name { get; set; } = string.Empty;
    public string ImageReference { get; set; } = string.Empty;
}