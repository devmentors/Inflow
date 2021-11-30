using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations;

internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.Ignore(x => x.Events);
        builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
            
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new WalletId(x));
            
        builder.Property(x => x.OwnerId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new OwnerId(x));

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Currency(x));
    }
}