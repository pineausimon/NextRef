using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRef.Application.Common.Abstractions;
public interface IQuery<TResponse> : IRequest<TResponse> { }
