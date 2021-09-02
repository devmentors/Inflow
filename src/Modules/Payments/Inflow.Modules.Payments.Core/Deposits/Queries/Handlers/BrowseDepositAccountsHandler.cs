using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.DAL;
using Inflow.Modules.Payments.Core.Deposits.DTO;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Payments.Core.Deposits.Queries.Handlers
{
    internal sealed class BrowseDepositAccountsHandler : IQueryHandler<BrowseDepositAccounts, Paged<DepositAccountDto>>
    {
        private readonly PaymentsDbContext _dbContext;

        public BrowseDepositAccountsHandler(PaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Paged<DepositAccountDto>> HandleAsync(BrowseDepositAccounts query,
            CancellationToken cancellationToken = default)
        {
            var accounts = _dbContext.DepositAccounts.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(query.Currency))
            {
                _ = new Currency(query.Currency);
                accounts = accounts.Where(x => x.Currency == query.Currency);
            }
            
            if (query.CustomerId.HasValue)
            {
                accounts = accounts.Where(x => x.CustomerId == query.CustomerId);
            }

            return accounts.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new DepositAccountDto
                {
                    AccountId = x.Id,
                    CustomerId = x.CustomerId,
                    Currency = x.Currency,
                    Iban = x.Iban,
                    CreatedAt = x.CreatedAt
                })
                .PaginateAsync(query, cancellationToken);
        }
    }
}