using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Repositories;

internal class CorporateOwnerRepository : ICorporateOwnerRepository
{
    private readonly WalletsDbContext _context;
    private readonly DbSet<CorporateOwner> _owners;
        
    public CorporateOwnerRepository(WalletsDbContext context)
    {
        _context = context;
        _owners = _context.CorporateOwners;
    }

        
    public Task<CorporateOwner> GetAsync(OwnerId id)
        => _owners.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(CorporateOwner owner)
    {
        await _owners.AddAsync(owner);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CorporateOwner owner)
    {
        _owners.Update(owner);
        await _context.SaveChangesAsync();
    }
}