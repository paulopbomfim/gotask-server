namespace GoTask.Exceptions.ExceptionBase;

public abstract class GoTaskException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}