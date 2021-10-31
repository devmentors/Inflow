using System.Reflection;
using Inflow.Services.Customers.Core.Clients;
using Inflow.Services.Customers.Core.DAL;
using Inflow.Services.Customers.Core.DAL.Repositories;
using Inflow.Services.Customers.Core.Domain.Repositories;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Storage;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure;
using Inflow.Shared.Infrastructure.Api;
using Inflow.Shared.Infrastructure.Auth;
using Inflow.Shared.Infrastructure.Commands;
using Inflow.Shared.Infrastructure.Contexts;
using Inflow.Shared.Infrastructure.Dispatchers;
using Inflow.Shared.Infrastructure.Events;
using Inflow.Shared.Infrastructure.Exceptions;
using Inflow.Shared.Infrastructure.Messaging;
using Inflow.Shared.Infrastructure.Messaging.Outbox;
using Inflow.Shared.Infrastructure.Messaging.RabbitMQ;
using Inflow.Shared.Infrastructure.Modules;
using Inflow.Shared.Infrastructure.Postgres;
using Inflow.Shared.Infrastructure.Queries;
using Inflow.Shared.Infrastructure.Serialization;
using Inflow.Shared.Infrastructure.Services;
using Inflow.Shared.Infrastructure.Storage;
using Inflow.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Inflow.Services.Customers.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            services.AddCorsPolicy();
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Customers API",
                    Version = "v1"
                });
            });
            
            var appOptions = services.GetOptions<AppOptions>("app");
            services.AddSingleton(appOptions);
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<IRequestStorage, RequestStorage>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
            services.AddAuth();
            services.AddErrorHandling();
            services.AddCommands(assemblies);
            services.AddEvents(assemblies);
            services.AddQueries(assemblies);
            services.AddContext();
            services.AddMessaging();
            services.AddRabbitMQ();
            services.AddModuleRequests(assemblies);
            services.AddSingleton<IClock, UtcClock>();
            services.AddSingleton<IDispatcher, InMemoryDispatcher>();
            services.AddPostgres();
            services.AddOutbox();
            services.AddHostedService<DbContextAppInitializer>();
            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("customers", x => x.RequireClaim("permissions", "customers"));
            });
            
            return services
                .AddSingleton<IUserApiClient, UserApiClient>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddPostgres<CustomersDbContext>()
                .AddOutbox<CustomersDbContext>()
                .AddUnitOfWork<CustomersUnitOfWork>();
        }
    }
}