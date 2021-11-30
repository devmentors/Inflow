using System;
using Inflow.Modules.Payments.Core.Withdrawals.Exceptions;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Withdrawals.Domain.Entities;

internal class Withdrawal
{
    public Guid Id { get; private set; }
    public WithdrawalStatus Status { get; private set; }
    public Guid AccountId { get; private set; }
    public WithdrawalAccount Account { get; private set; }
    public Amount Amount { get; private set; }
    public Currency Currency { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }

    private Withdrawal()
    {
    }

    public Withdrawal(Guid id, Guid accountId, Amount amount, Currency currency, DateTime createdAt)
    {
        Id = id;
        AccountId = accountId;
        Amount = amount;
        Currency = currency;
        CreatedAt = createdAt;
        Status = WithdrawalStatus.Started;
    }
        
    public void Complete(DateTime completedAt)
    {
        if (Status != WithdrawalStatus.Started)
        {
            throw new CannotCompleteWithdrawalException(Id);
        }
            
        Status = WithdrawalStatus.Completed;
        ProcessedAt = completedAt;
    }
        
    public void Reject(DateTime rejectedAt)
    {
        if (Status != WithdrawalStatus.Started)
        {
            throw new CannotRejectWithdrawalException(Id);
        }
            
        Status = WithdrawalStatus.Rejected;
        ProcessedAt = rejectedAt;
    }
}