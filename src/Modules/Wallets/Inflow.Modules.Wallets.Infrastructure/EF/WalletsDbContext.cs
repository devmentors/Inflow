using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Infrastructure.Messaging.Outbox;

namespace Inflow.Modules.Wallets.Infrastructure.EF;

internal class WalletsDbContext : DbContext
{
    public DbSet<InboxMessage> Inbox { get; set; }
    public DbSet<OutboxMessage> Outbox { get; set; }
    public DbSet<CorporateOwner> CorporateOwners { get; set; }
    public DbSet<IndividualOwner> IndividualOwners { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<IncomingTransfer> IncomingTransfers { get; set; }
    public DbSet<OutgoingTransfer> OutgoingTransfers { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
        
    public WalletsDbContext(DbContextOptions<WalletsDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=SqliteDBFiles\SqliteWalletsDB.db");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wallets");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}