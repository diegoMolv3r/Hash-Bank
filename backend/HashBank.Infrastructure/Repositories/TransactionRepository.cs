using HashBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HashBank.Infrastructure.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }

    internal class TransactionRepository : ITransactionRepository
    {
        public void Add(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
