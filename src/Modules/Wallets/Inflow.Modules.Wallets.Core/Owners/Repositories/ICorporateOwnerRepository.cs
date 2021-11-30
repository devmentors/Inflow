using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Types;

namespace Inflow.Modules.Wallets.Core.Owners.Repositories;

internal interface ICorporateOwnerRepository
{
    Task<CorporateOwner> GetAsync(OwnerId id);
    Task AddAsync(CorporateOwner owner);
    Task UpdateAsync(CorporateOwner owner);
}