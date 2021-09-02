using System;
using Microsoft.AspNetCore.Http;
using Inflow.Shared.Abstractions.Contexts;
using Inflow.Shared.Infrastructure.Api;

namespace Inflow.Shared.Infrastructure.Contexts
{
    public class Context : IContext
    {
        public Guid RequestId { get; } = Guid.NewGuid();
        public Guid CorrelationId { get; }
        public string TraceId { get; }
        public string IpAddress { get; }
        public string UserAgent { get; }
        public IIdentityContext Identity { get; }

        public Context() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}", null)
        {
        }

        public Context(HttpContext context) : this(context.TryGetCorrelationId(), context.TraceIdentifier,
            new IdentityContext(context.User), context.GetUserIpAddress(),
            context.Request.Headers["user-agent"])
        {
        }

        public Context(Guid? correlationId, string traceId, IIdentityContext identity = null, string ipAddress = null,
            string userAgent = null)
        {
            CorrelationId = correlationId ?? Guid.NewGuid();
            TraceId = traceId;
            Identity = identity ?? IdentityContext.Empty;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public static IContext Empty => new Context();
    }
}