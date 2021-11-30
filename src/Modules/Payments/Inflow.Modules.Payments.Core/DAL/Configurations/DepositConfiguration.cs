using Inflow.Modules.Payments.Core.Deposits.Domain.Entities;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Payments.Core.DAL.Configurations;

internal class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.Property(x => x.Amount).IsRequired()
            .HasConversion(x => x.Value, x => new Amount(x));
            
        builder.Property(x => x.Currency).IsRequired()
            .HasConversion(x => x.Value, x => new Currency(x));
            
        // For PostgreSQL UseXminAsConcurrencyToken() can be used instead
        builder.Property(x => x.ProcessedAt).IsConcurrencyToken();
    }
}