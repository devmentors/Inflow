using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Owners.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations
{
    internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");
            
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new OwnerId(x));
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasConversion(x => x.Value, x => new OwnerName(x));
            
            builder
                .HasDiscriminator<string>("Type")
                .HasValue<CorporateOwner>(nameof(CorporateOwner))
                .HasValue<IndividualOwner>(nameof(IndividualOwner));
        }
    }
}