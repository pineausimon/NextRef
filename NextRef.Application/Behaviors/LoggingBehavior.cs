using MediatR;
using Microsoft.Extensions.Logging;

namespace NextRef.Application.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {RequestType} with data: {@Request}", typeof(TRequest).Name, request);

        var response = await next(cancellationToken);

        _logger.LogInformation("Handled {RequestType} with response: {@Response}", typeof(TRequest).Name, response);

        return response;
    }
}