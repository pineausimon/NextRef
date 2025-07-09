using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRef.Application.Common.Abstractions;

public interface ICustomMediator
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command);
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
}