using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.DAL;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Entities;
using Inflow.Modules.Payments.Core.Withdrawals.DTO;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Payments.Core.Withdrawals.Queries.Handlers;

internal sealed class BrowseWithdrawalsHandler : IQueryHandler<BrowseWithdrawals, Paged<WithdrawalDto>>
{
    private readonly PaymentsDbContext _dbContext;

    public BrowseWithdrawalsHandler(PaymentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Paged<WithdrawalDto>> HandleAsync(BrowseWithdrawals query, CancellationToken cancellationToken = default)
    {
        var withdrawals = _dbContext.Withdrawals.AsQueryable();
            
        if (!string.IsNullOrWhiteSpace(query.Currency))
        {
            _ = new Currency(query.Currency);
            withdrawals = withdrawals.Where(x => x.Currency == query.Currency);
        }

        if (!string.IsNullOrWhiteSpace(query.Status) &&
            Enum.TryParse<WithdrawalStatus>(query.Status, true, out var status))
        {
            withdrawals = withdrawals.Where(x => x.Status == status);
        }
            
        if (query.AccountId.HasValue)
        {
            withdrawals = withdrawals.Where(x => x.AccountId == query.AccountId &&
                                                 (!query.CustomerId.HasValue || x.Account.CustomerId == query.CustomerId));
        }

        if (query.CustomerId.HasValue)
        {
            withdrawals = withdrawals.Where(x => x.Account.CustomerId == query.CustomerId);
        }

        return withdrawals.AsNoTracking()
            .Include(x => x.Account)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new WithdrawalDto
            {
                WithdrawalId = x.Id,
                AccountId = x.AccountId,
                CustomerId = x.Account.CustomerId,
                Amount = x.Amount,
                Currency = x.Currency,
                Status = x.Status.ToString().ToLowerInvariant(),
                CreatedAt = x.CreatedAt,
                ProcessedAt = x.ProcessedAt
            })
            .PaginateAsync(query, cancellationToken);
    }
}