using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Entities;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Repositories;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Payments.Core.DAL.Repositories;

internal class WithdrawalAccountRepository : IWithdrawalAccountRepository
{
    private readonly PaymentsDbContext _context;
    private readonly DbSet<WithdrawalAccount> _withdrawalAccounts;

    public WithdrawalAccountRepository(PaymentsDbContext context)
    {
        _context = context;
        _withdrawalAccounts = _context.WithdrawalAccounts;
    }
        
    public Task<bool> ExistsAsync(Guid customerId, Currency currency)
        => _withdrawalAccounts.AnyAsync(x => x.CustomerId == customerId && x.Currency.Equals(currency));
        
    public  Task<WithdrawalAccount> GetAsync(Guid id)
        => _withdrawalAccounts.SingleOrDefaultAsync(x => x.Id == id);
        
    public Task<WithdrawalAccount> GetAsync(Guid customerId, Currency currency)
        => _withdrawalAccounts.SingleOrDefaultAsync(x => x.CustomerId == customerId && x.Currency.Equals(currency));

    public async Task AddAsync(WithdrawalAccount withdrawalAccount)
    {
        await _withdrawalAccounts.AddAsync(withdrawalAccount);
        await _context.SaveChangesAsync();
    }
}