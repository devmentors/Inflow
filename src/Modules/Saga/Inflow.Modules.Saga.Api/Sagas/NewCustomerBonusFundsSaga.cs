using System;
using System.Threading.Tasks;
using Chronicle;
using Inflow.Modules.Saga.Api.Messages;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Saga.Api.Sagas;

internal sealed class NewCustomerBonusFundsSagaData
{
    public DateTime VerifiedAt { get; set; }
    public Guid WalletId { get; set; }
    public string Currency { get; set; }
    public bool DepositCompleted { get; set; }
}

internal sealed class NewCustomerBonusFundsSaga : Saga<NewCustomerBonusFundsSagaData>,
    ISagaStartAction<CustomerVerified>,
    ISagaAction<WalletAdded>,
    ISagaAction<DepositCompleted>,
    ISagaAction<FundsAdded>
{
    private const decimal BonusFunds = 10;
    private const string TransferName = "new_customer_bonus";
    private readonly IMessageBroker _messageBroker;
    private readonly IClock _clock;

    public NewCustomerBonusFundsSaga(IMessageBroker messageBroker, IClock clock)
    {
        _messageBroker = messageBroker;
        _clock = clock;
    }

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            CustomerVerified m => m.CustomerId.ToString(),
            DepositCompleted m => m.CustomerId.ToString(),
            WalletAdded m => m.OwnerId.ToString(),
            FundsAdded m => m.OwnerId.ToString(),
            _ => base.ResolveId(message, context)
        };

    public Task HandleAsync(CustomerVerified message, ISagaContext context)
    {
        Data.VerifiedAt = _clock.CurrentDate();
        return Task.CompletedTask;
    }

    public Task CompensateAsync(CustomerVerified message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(WalletAdded message, ISagaContext context)
    {
        Data.WalletId = message.WalletId;
        Data.Currency = message.Currency;
        return Task.CompletedTask;
    }

    public Task CompensateAsync(WalletAdded message, ISagaContext context)
        => Task.CompletedTask;

    public async Task HandleAsync(DepositCompleted message, ISagaContext context)
    {
        if (Data.DepositCompleted)
        {
            return;
        }

        var now = _clock.CurrentDate();
        if (now > Data.VerifiedAt.AddDays(7))
        {
            await CompleteAsync();
            return;
        }

        Data.DepositCompleted = true;
        await _messageBroker.PublishAsync(new AddFunds(Data.WalletId, Data.Currency, BonusFunds,
            TransferName));
    }

    public Task CompensateAsync(DepositCompleted message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(FundsAdded message, ISagaContext context)
    {
        if (message.TransferName == TransferName)
        {
            return CompleteAsync();
        }

        return Task.CompletedTask;
    }

    public Task CompensateAsync(FundsAdded message, ISagaContext context)
        => Task.CompletedTask;
}