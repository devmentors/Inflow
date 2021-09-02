using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Modules.Wallets.Core.Wallets.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations
{
    internal class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder
                .HasDiscriminator<string>("Type")
                .HasValue<IncomingTransfer>(nameof(IncomingTransfer))
                .HasValue<OutgoingTransfer>(nameof(OutgoingTransfer));
            
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new TransferId(x));

            builder.Property(x => x.WalletId)
                .IsRequired()
                .HasConversion(x => x.Value, x => new WalletId(x));
            
            builder.Property(x => x.Currency)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Currency(x));
            
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Amount(x));
            
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => new TransferName(x));
            
            builder.Property(x => x.Metadata)
                .HasMaxLength(1000)
                .HasConversion(x => x.Value, x => new TransferMetadata(x));
        }
    }
}