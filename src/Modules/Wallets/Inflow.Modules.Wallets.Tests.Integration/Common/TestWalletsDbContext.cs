using Inflow.Modules.Wallets.Infrastructure.EF;
using Inflow.Shared.Tests;

namespace Inflow.Modules.Wallets.Tests.Integration.Common
{
    internal class TestWalletsDbContext : TestDbContext<WalletsDbContext>
    {
        protected override WalletsDbContext Init(string connectionString)
            => new(DbHelper.GetOptions<WalletsDbContext>());
    }
}