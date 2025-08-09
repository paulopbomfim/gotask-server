namespace GoTask.Application.Services.User;

public interface IUserContextService
{
    string Role { get; set; }
    string UserIdentification { get; set; }
}