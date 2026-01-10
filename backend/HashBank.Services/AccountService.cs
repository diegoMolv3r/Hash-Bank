using HashBank.Infrastructure.Repositories;
using HashBank.Services.DTOs;

namespace HashBank.Services
{
    public interface IAccountService
    {
        Task<int> CreateAccountAsync(int userId);
        Task<AccountDto> GetAccountByUserIdAsync(int userId);
        Task<AccountDto> GetAccountByCbuAsync(string cbu);
        Task<bool> AccountExistsAsync(int accountId);
        Task<bool> IsAccountActiveAsync(int accountId);
    }
    public class AccountService(AccountRepository _accountRepository, IUnitOfWork _unitOfWork) : IAccountService
    {
        public Task<bool> AccountExistsAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAccountAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto> GetAccountByCbuAsync(string cbu)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto> GetAccountByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccountActiveAsync(int accountId)
        {
            throw new NotImplementedException();
        }

    }
}
