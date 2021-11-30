using System.Runtime.CompilerServices;
using Inflow.Modules.Wallets.Application.Wallets.Storage;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Inflow.Modules.Wallets.Infrastructure.EF;
using Inflow.Modules.Wallets.Infrastructure.EF.Repositories;
using Inflow.Modules.Wallets.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Infrastructure.Messaging.Outbox;
using Inflow.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Api")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Contract")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.EndToEnd")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Integration")]
[assembly: InternalsVisibleTo("Inflow.Modules.Wallets.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Inflow.Modules.Wallets.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<ITransferStorage, TransferStorage>()
            .AddScoped<IWalletStorage, WalletStorage>()
            .AddScoped<ICorporateOwnerRepository, CorporateOwnerRepository>()
            .AddScoped<IIndividualOwnerRepository, IndividualOwnerRepository>()
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddPostgres<WalletsDbContext>()
            .AddOutbox<WalletsDbContext>()
            .AddUnitOfWork<WalletsUnitOfWork>();
    }
}