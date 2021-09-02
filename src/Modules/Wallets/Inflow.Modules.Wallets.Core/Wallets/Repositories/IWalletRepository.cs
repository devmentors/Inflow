using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Wallets.Core.Wallets.Repositories
{
    internal interface IWalletRepository
    {
        Task<Wallet> GetAsync(WalletId id);
        Task<Wallet> GetAsync(OwnerId ownerId, Currency currency);
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
    }
}