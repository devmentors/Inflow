using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Deposits.Domain.Entities;
using Inflow.Modules.Payments.Core.Deposits.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Payments.Core.DAL.Repositories
{
    internal class DepositRepository : IDepositRepository
    {
        private readonly PaymentsDbContext _context;
        private readonly DbSet<Deposit> _deposits;

        public DepositRepository(PaymentsDbContext context)
        {
            _context = context;
            _deposits = _context.Deposits;
        }

        public  Task<Deposit> GetAsync(Guid id)
            => _deposits
                .Include(x => x.Account)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Deposit deposit)
        {
            await _deposits.AddAsync(deposit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Deposit deposit)
        {
            _deposits.Update(deposit);
            await _context.SaveChangesAsync();
        }
    }
}