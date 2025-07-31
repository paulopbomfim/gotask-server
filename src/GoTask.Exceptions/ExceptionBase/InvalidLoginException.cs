using System.Net;

namespace GoTask.Exceptions.ExceptionBase;

public class InvalidLoginException() : GoTaskException(ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID)
{
    public override int StatusCode { get; } = (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}