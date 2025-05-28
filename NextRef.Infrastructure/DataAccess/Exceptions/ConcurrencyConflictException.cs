namespace NextRef.Infrastructure.DataAccess.Exceptions;
public class ConcurrencyConflictException : Exception
{
    public ConcurrencyConflictException()
        : base("Une erreur de concurrence s'est produite : la ressource a été modifiée par un autre processus.")
    {
    }

    public ConcurrencyConflictException(string message) : base(message)
    {
    }

    public ConcurrencyConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
}