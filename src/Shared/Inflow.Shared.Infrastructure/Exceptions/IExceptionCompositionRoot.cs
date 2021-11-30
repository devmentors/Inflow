using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}