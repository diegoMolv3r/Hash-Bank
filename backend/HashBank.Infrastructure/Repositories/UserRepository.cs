using HashBank.Domain.Entities;
using HashBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HashBank.Infrastructure.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }
    public class UserRepository(HashBankDbContext context) : IUserRepository 
    {
        HashBankDbContext _context = context;

        public void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
