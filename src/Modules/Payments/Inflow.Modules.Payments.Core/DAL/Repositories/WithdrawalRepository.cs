using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Entities;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Payments.Core.DAL.Repositories
{
    internal class WithdrawalRepository : IWithdrawalRepository
    {
        private readonly PaymentsDbContext _context;
        private readonly DbSet<Withdrawal> _withdrawals;

        public WithdrawalRepository(PaymentsDbContext context)
        {
            _context = context;
            _withdrawals = _context.Withdrawals;
        }

        public  Task<Withdrawal> GetAsync(Guid id)
            => _withdrawals
                .Include(x => x.Account)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Withdrawal withdrawal)
        {
            await _withdrawals.AddAsync(withdrawal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Withdrawal withdrawal)
        {
            _withdrawals.Update(withdrawal);
            await _context.SaveChangesAsync();
        }
    }
}