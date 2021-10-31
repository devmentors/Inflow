using Inflow.Services.Customers.Core;
using Inflow.Services.Customers.Core.Events.External;
using Inflow.Shared.Infrastructure;
using Inflow.Shared.Infrastructure.Auth;
using Inflow.Shared.Infrastructure.Contexts;
using Inflow.Shared.Infrastructure.Exceptions;
using Inflow.Shared.Infrastructure.Logging;
using Inflow.Shared.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace Inflow.Services.Customers.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCore();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("cors");
            app.UseCorrelationId();
            app.UseErrorHandling();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Customers API";
            });
            app.UseAuth();
            app.UseContext();
            app.UseLogging();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Customers API"));
            });

            app.Subscriptions()
                .SubscribeEvent<SignedUp>()
                .SubscribeEvent<UserStateUpdated>();
        }
    }
}