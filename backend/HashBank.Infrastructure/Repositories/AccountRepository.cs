using HashBank.Domain.Entities;
using HashBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HashBank.Infrastructure.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<List<Account>> GetAccountsByAliasAsync(string alias);
        public Task<List<Account>> GetAccountsByCbuAsync(string cbu);
        public Task<List<Account>> GetAccountsByUserIdAsync(int userId);
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
        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }


        public async Task<List<Account>> GetAccountsByAliasAsync(string alias) 
        {
            //List<Account>? accounts = await _context.Accounts.Where(a => a.Alias == alias).ToListAsync();
            //return accounts;
            // todavia no tengo alias como atributo
            return new List<Account>();
        }
        public async Task<List<Account>> GetAccountsByCbuAsync(string cbu) 
        {
            List<Account>? accounts = await _context.Accounts.Where(a => a.CBU == cbu).ToListAsync();
            return accounts;
        }
        public async Task<List<Account>> GetAccountsByUserIdAsync(int userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
