using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Modules.Users.Core.DAL;
using Inflow.Modules.Users.Core.DAL.Repositories;
using Inflow.Modules.Users.Core.Entities;
using Inflow.Modules.Users.Core.Repositories;
using Inflow.Modules.Users.Core.Services;
using Inflow.Shared.Infrastructure;
using Inflow.Shared.Infrastructure.Messaging.Outbox;
using Inflow.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("Inflow.Modules.Users.Api")]
[assembly: InternalsVisibleTo("Inflow.Modules.Users.Tests.Integration")]
[assembly: InternalsVisibleTo("Inflow.Modules.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]


namespace Inflow.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        return services
            .AddSingleton<IUserRequestStorage, UserRequestStorage>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddPostgres<UsersDbContext>()
            .AddOutbox<UsersDbContext>()
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>();
    }
}