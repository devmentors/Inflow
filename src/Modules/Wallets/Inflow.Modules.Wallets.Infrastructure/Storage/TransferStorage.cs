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

internal sealed class TransferStorage : ITransferStorage
{
    private readonly DbSet<Transfer> _transfers;

    public TransferStorage(WalletsDbContext dbContext)
    {
        _transfers = dbContext.Transfers;
    }

    public Task<Transfer> FindAsync(Expression<Func<Transfer, bool>> expression)
        => _transfers
            .AsNoTracking()
            .AsQueryable()
            .Where(expression)
            .SingleOrDefaultAsync();

    public Task<Paged<Transfer>> BrowseAsync(Expression<Func<Transfer, bool>> expression, IPagedQuery query)
        => _transfers
            .AsNoTracking()
            .AsQueryable()
            .Where(expression)
            .OrderBy(x => x.CreatedAt)
            .PaginateAsync(query);
}