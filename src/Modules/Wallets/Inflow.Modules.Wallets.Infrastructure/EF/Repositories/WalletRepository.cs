using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Repositories;

internal class WalletRepository : IWalletRepository
{
    private readonly WalletsDbContext _context;
    private readonly DbSet<Wallet> _wallets;
        
    public WalletRepository(WalletsDbContext context)
    {
        _context = context;
        _wallets = _context.Wallets;
    }
        
    public Task<Wallet> GetAsync(WalletId id)
        => _wallets
            .Include(x => x.Transfers)
            .SingleOrDefaultAsync(x => x.Id == id);

    public Task<Wallet> GetAsync(OwnerId ownerId, Currency currency)
        => _wallets
            .Include(x => x.Transfers)
            .SingleOrDefaultAsync(x => x.OwnerId == ownerId && x.Currency.Equals(currency));

    public async Task AddAsync(Wallet wallet)
    {
        await _wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }
}