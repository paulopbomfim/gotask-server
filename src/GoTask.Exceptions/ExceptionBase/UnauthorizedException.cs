using System.Net;

namespace GoTask.Exceptions.ExceptionBase;

public class UnauthorizedException() : GoTaskException(ErrorMessagesResource.UNAUTHORIZED_ERROR)
{
    public override int StatusCode { get; } = (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}