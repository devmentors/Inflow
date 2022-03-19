using System.Collections.Generic;
using Inflow.Modules.Customers.Core;
using Inflow.Modules.Customers.Core.DTO;
using Inflow.Modules.Customers.Core.Events.External;
using Inflow.Modules.Customers.Core.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Abstractions.Modules;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Contracts;
using Inflow.Shared.Infrastructure.Modules;

namespace Inflow.Modules.Customers.Api;

internal class CustomersModule : IModule
{
    public string Name { get; } = "Customers";
        
    public IEnumerable<string> Policies { get; } = new[]
    {
        "customers"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseContracts()
            .Register<SignedUpContract>()
            .Register<UserStateUpdated>();
        
        app.UseModuleRequests()
            .Subscribe<GetCustomer, CustomerDetailsDto>("customers/get",
                (query, serviceProvider, cancellationToken)
                    => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}