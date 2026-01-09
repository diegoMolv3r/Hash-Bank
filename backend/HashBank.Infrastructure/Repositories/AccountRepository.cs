using HashBank.Domain.Entities;
using HashBank.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace HashBank.Infrastructure.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public async Task<Account> GetAccountByAliasAsync(string alias);
        public async Task<Account> GetAccountByCbuAsync(string cbu);
        public async Task<Account> GetAccountByUserIdAsync(int userId);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly HashBankDbContext _context;    
        public void Add(Account entity)
        {
            if(entity != null)
            {
                _context.Accounts.Add(entity);
                _context.SaveChanges();
            }
        }
        public void Remove(Account entity)
        {
            if (entity != null)
            {
                entity.IsActive = false;
                _context.SaveChanges();
            }
        }
        public void Update(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> FindAsync(Expression<Func<Account, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            return Task.FromResult(_context.Accounts.AsEnumerable());
        }

        public Task<Account> GetByIdAsync(int id)
        {
            return Task.FromResult(_context.Accounts.Find(id));
        }

        
        public async Task<Account> GetAccountByAliasAsync(string alias) 
        {
            return null; // _context.Accounts.Where(a => a. == alias).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByCbuAsync(string cbu) 
        {
            return await _context.Accounts.Where(a => a.CBU == cbu).FirstOrDefault();
        }
        public async Task<List<Account>> GetAccountsByUserIdAsync(int userId) 
        { 
            return await _context.Accounts.Where(a => a.Id == userId).GetAllAsync();
        }
    }
}
