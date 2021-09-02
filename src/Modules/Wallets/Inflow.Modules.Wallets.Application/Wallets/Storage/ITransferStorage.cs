using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Storage
{
    internal interface ITransferStorage
    {
        Task<Transfer> FindAsync(Expression<Func<Transfer, bool>> expression);
        Task<Paged<Transfer>> BrowseAsync(Expression<Func<Transfer, bool>> expression, IPagedQuery query);
    }
}
