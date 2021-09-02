using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations
{
    internal class IndividualOwnerConfiguration : IEntityTypeConfiguration<IndividualOwner>
    {
        public void Configure(EntityTypeBuilder<IndividualOwner> builder)
        {
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasConversion(x => x.Value, x => new FullName(x));
        }
    }
}