using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Api")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Infrastructure")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Contract")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.EndToEnd")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Integration")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Inflow.Modules.Wallets.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}