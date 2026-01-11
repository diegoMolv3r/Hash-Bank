using HashBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBank.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
    public class UnitOfWork(HashBankDbContext context) : IUnitOfWork
    {
        HashBankDbContext _context = context;

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
