using Inflow.Modules.Customers.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Infrastructure.Messaging.Outbox;

namespace Inflow.Modules.Customers.Core.DAL
{
    internal class CustomersDbContext : DbContext
    {
        public DbSet<InboxMessage> Inbox { get; set; }
        public DbSet<OutboxMessage> Outbox { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("customers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}