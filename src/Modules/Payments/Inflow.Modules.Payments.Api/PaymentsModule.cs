using System.Collections.Generic;
using Inflow.Modules.Payments.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Abstractions.Modules;

namespace Inflow.Modules.Payments.Api;

internal class PaymentsModule : IModule
{
    public string Name { get; } = "Payments";
        
    public IEnumerable<string> Policies { get; } = new[]
    {
        "deposits", "withdrawals"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }
        
    public void Use(IApplicationBuilder app)
    {
    }
}