using System.Collections.Generic;
using Inflow.Modules.Payments.Core;
using Inflow.Modules.Payments.Core.Deposits.Events.External;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Abstractions.Modules;
using Inflow.Shared.Infrastructure.Messaging;

namespace Inflow.Modules.Payments.Api
{
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
            app.Subscriptions()
                .SubscribeEvent<CustomerCompleted>()
                .SubscribeEvent<CustomerLocked>()
                .SubscribeEvent<CustomerUnlocked>()
                .SubscribeEvent<CustomerVerified>();
        }
    }
}