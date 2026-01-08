using HashBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HashBank.Infrastructure.Persistence
{
    public class HashBankDbContext : DbContext
    {
        public HashBankDbContext(DbContextOptions<HashBankDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(p => p.Balance)
                .HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
