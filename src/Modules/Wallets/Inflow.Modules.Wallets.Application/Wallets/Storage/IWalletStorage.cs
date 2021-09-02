using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Storage
{
    internal interface IWalletStorage
    {
        Task<Wallet> FindAsync(Expression<Func<Wallet, bool>> expression);
        Task<Paged<Wallet>> BrowseAsync(Expression<Func<Wallet, bool>> expression, IPagedQuery query);
    }
}