using Inflow.Services.Customers.Core.Domain.Entities;
using Inflow.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Services.Customers.Core.DAL
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<InboxMessage> Inbox { get; set; }
        public DbSet<OutboxMessage> Outbox { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}