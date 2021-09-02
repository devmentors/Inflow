using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Repositories
{
    internal class IndividualOwnerRepository : IIndividualOwnerRepository
    {
        private readonly WalletsDbContext _context;
        private readonly DbSet<IndividualOwner> _owners;
        
        public IndividualOwnerRepository(WalletsDbContext context)
        {
            _context = context;
            _owners = _context.IndividualOwners;
        }
        
        public Task<IndividualOwner> GetAsync(OwnerId id)
            => _owners.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(IndividualOwner owner)
        {
            await _owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IndividualOwner owner)
        {
            _owners.Update(owner);
            await _context.SaveChangesAsync();
        }
    }
}