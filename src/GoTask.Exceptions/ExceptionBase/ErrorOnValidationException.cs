using System.Net;

namespace GoTask.Exceptions.ExceptionBase;

public class ErrorOnValidationException(List<string> errors) : GoTaskException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public override List<string> GetErrors()
    {
        return errors;
    }
}