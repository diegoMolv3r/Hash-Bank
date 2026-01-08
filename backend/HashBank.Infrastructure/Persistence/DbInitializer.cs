using HashBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HashBank.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(HashBankDbContext context)
        {
            context.Database.Migrate();

            if (context.Transactions.Any())
            {
                return;   
            }

            var transactions = new Transaction[]
            {
                new Transaction { Amount = 150.00m, TransactionDate = DateTime.Now.AddDays(-2), Description = "Depósito Inicial" },
                new Transaction { Amount = -25.50m,  TransactionDate = DateTime.Now.AddDays(-1), Description = "Compra Café" },
                new Transaction { Amount = 5000.00m, TransactionDate = DateTime.Now,            Description = "Nómina" }
            };

            foreach (var t in transactions)
            {
                context.Transactions.Add(t);
            }

            context.SaveChanges();
        }
    }
}
