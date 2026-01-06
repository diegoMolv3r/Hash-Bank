using HashBank.Domain.Entities;

namespace HashBank.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        public Account GetById(int id);
        public void Update(Account account);

        public void AddTransaction(Transfer transfer);
    }

    public class AccountRepository : IAccountRepository
    {
        public void AddTransaction(Transfer transfer)
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
