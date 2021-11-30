using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.Storage;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Wallets.Infrastructure.Storage;

internal sealed class WalletStorage : IWalletStorage
{
    private readonly DbSet<Wallet> _wallets;

    public WalletStorage(WalletsDbContext dbContext)
    {
        _wallets = dbContext.Wallets;
    }

    public Task<Wallet> FindAsync(Expression<Func<Wallet, bool>> expression)
        => _wallets
            .AsNoTracking()
            .AsQueryable()
            .Where(expression)
            .Include(x => x.Transfers)
            .SingleOrDefaultAsync();

    public Task<Paged<Wallet>> BrowseAsync(Expression<Func<Wallet, bool>> expression, IPagedQuery query)
        => _wallets
            .AsNoTracking()
            .AsQueryable()
            .Where(expression)
            .OrderBy(x => x.CreatedAt)
            .PaginateAsync(query);
}