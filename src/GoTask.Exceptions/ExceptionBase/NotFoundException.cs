using System.Net;

namespace GoTask.Exceptions.ExceptionBase;

public class NotFoundException() : GoTaskException(ErrorMessagesResource.NOT_FOUND_ERROR)
{
    public override int StatusCode { get; } = (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}