using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations;

internal class CorporateOwnerConfiguration : IEntityTypeConfiguration<CorporateOwner>
{
    public void Configure(EntityTypeBuilder<CorporateOwner> builder)
    {
        builder.Property(x => x.TaxId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new TaxId(x));
    }
}