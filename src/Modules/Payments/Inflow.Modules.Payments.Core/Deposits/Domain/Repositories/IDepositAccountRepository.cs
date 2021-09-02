using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Deposits.Domain.Entities;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Deposits.Domain.Repositories
{
    internal interface IDepositAccountRepository
    {
        Task<DepositAccount> GetAsync(Guid id);
        Task<DepositAccount> GetAsync(Guid customerId, Currency currency);
        Task AddAsync(DepositAccount depositAccount);
    }
}