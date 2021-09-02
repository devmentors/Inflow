using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations
{
    internal class IncomingTransferConfiguration : IEntityTypeConfiguration<IncomingTransfer>
    {
        public void Configure(EntityTypeBuilder<IncomingTransfer> builder)
        {
        }
    }
}