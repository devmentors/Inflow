using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Wallets.Infrastructure.EF;

internal class WalletsUnitOfWork : PostgresUnitOfWork<WalletsDbContext>
{
    public WalletsUnitOfWork(WalletsDbContext dbContext) : base(dbContext)
    {
    }
}