using GoTask.Communication.Responses;
using GoTask.Exceptions;
using GoTask.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Diagnostics;

namespace GoTask.API.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, response) = exception switch
        {
            GoTaskException goTaskException => HandleProjectException(goTaskException),
            _ => HandleUnknownError()
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        
        return true;
    }

    private static (int StatusCode, ErrorResponse Response) HandleProjectException(GoTaskException exception)
    {
        return (exception.StatusCode, new ErrorResponse(exception.GetErrors()));
    }

    private static (int StatusCode, ErrorResponse Response) HandleUnknownError()
    {
        return (StatusCodes.Status500InternalServerError, 
            new ErrorResponse(ErrorMessagesResource.UNKNOWN_ERROR));
    }
}