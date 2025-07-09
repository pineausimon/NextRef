using MediatR;
using NextRef.Application.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRef.Infrastructure.Mediator;
public class MediatrAdapter : ICustomMediator
{
    private readonly IMediator _mediator;

    public MediatrAdapter(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command)
        => _mediator.Send(command);

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query)
        => _mediator.Send(query);
}
