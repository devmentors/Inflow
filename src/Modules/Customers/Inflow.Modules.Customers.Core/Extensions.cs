using System.Runtime.CompilerServices;
using Inflow.Modules.Customers.Core.Clients;
using Inflow.Modules.Customers.Core.DAL;
using Inflow.Modules.Customers.Core.DAL.Repositories;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Infrastructure.Messaging.Outbox;
using Inflow.Shared.Infrastructure.Postgres;
using Inflow.Shared.Infrastructure.Sqlite;

[assembly: InternalsVisibleTo("Inflow.Modules.Customers.Api")]
[assembly: InternalsVisibleTo("Inflow.Modules.Customers.Tests.Integration")]
[assembly: InternalsVisibleTo("Inflow.Modules.Customers.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Inflow.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserApiClient, UserApiClient>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddSqlite<CustomersDbContext>()
            //.AddPostgres<CustomersDbContext>()
            .AddOutbox<CustomersDbContext>()
            .AddUnitOfWork<CustomersUnitOfWork>();
    }
}