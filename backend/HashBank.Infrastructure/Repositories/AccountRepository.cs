using HashBank.Domain.Entities;
using System.Linq.Expressions;

namespace HashBank.Infrastructure.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
    }

    public class AccountRepository : IAccountRepository
    {
        public void Add(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> FindAsync(Expression<Func<Account, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Account entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
