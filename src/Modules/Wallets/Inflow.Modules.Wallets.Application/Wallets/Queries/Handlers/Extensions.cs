using System;
using System.Linq;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Modules.Wallets.Core.Wallets.Entities;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries.Handlers;

internal static class Extensions
{
    public static TransferDto AsDto(this Transfer wallet)
        => wallet.Map<TransferDto>();

    public static TransferDetailsDto AsDetailsDto(this Transfer transfer)
    {
        var dto = transfer.Map<TransferDetailsDto>();
        dto.Metadata = transfer.Metadata;

        return dto;
    }

    private static T Map<T>(this Transfer transfer) where T : TransferDto, new()
        => new()
        {
            TransferId = transfer.Id,
            Amount = transfer.Amount,
            Currency = transfer.Currency,
            Name = transfer.Name,
            Type = transfer switch
            {
                IncomingTransfer => "incoming",
                OutgoingTransfer => "outgoing",
                _ => string.Empty
            },
            WalletId = transfer switch
            {
                IncomingTransfer t => t.WalletId,
                OutgoingTransfer t => t.WalletId,
                _ => Guid.Empty
            },
            CreatedAt = transfer.CreatedAt
        };
        
    public static WalletDto AsDto(this Wallet wallet)
        => wallet.Map<WalletDto>();

    public static WalletDetailsDto AsDetailsDto(this Wallet wallet)
    {
        var dto = wallet.Map<WalletDetailsDto>();
        dto.Amount = wallet.CurrentAmount();
        dto.Transfers = wallet.Transfers.Select(x => x.AsDto())
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        return dto;
    }

    private static T Map<T>(this Wallet wallet) where T : WalletDto, new()
        => new()
        {
            WalletId = wallet.Id,
            OwnerId = wallet.OwnerId,
            Currency = wallet.Currency,
            CreatedAt = wallet.CreatedAt,
        };
}