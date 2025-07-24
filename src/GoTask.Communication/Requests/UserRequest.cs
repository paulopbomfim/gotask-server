using GoTask.Communication.Enums;

namespace GoTask.Communication.Requests;

public record UserRequest()
{
    /// <summary>
    /// Nome do usuário.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Email do usuário.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Senha do usuário.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// Referência da imagem de perfil do usuário.
    /// </summary>
    public string ImageReference { get; set; } = string.Empty;
    /// <summary>
    /// Nível do usuário na organização.
    /// </summary>
    public OrganizationRole Role { get; set; } = OrganizationRole.Member;
    /// <summary>
    /// Organização à qual o usuário pertence.
    /// </summary>
    public long? OrganizationId { get; set; }
}