using NextRef.Application.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRef.Infrastructure.Mediator;
public class ManualMediator : ICustomMediator
{
    private readonly IServiceProvider _provider;

    public ManualMediator(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType);
        if (handler == null) throw new InvalidOperationException($"Handler not found for {command.GetType().Name}");
        var method = handlerType.GetMethod("Handle");
        return (Task<TResponse>)method.Invoke(handler, new object[] { command, CancellationToken.None });
    }

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType);
        if (handler == null) throw new InvalidOperationException($"Handler not found for {query.GetType().Name}");
        var method = handlerType.GetMethod("Handle");
        return (Task<TResponse>)method.Invoke(handler, new object[] { query, CancellationToken.None });
    }
}
