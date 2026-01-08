using HashBank.Domain.Entities;
using HashBank.Infrastructure.Repositories;

namespace HashBanck.Services
{
    public interface ITransferService
    {
        public Task PerformTransfer(int fromAccountId, int toAccountId, decimal amount);
    }
    public class TransferService(
        IAccountRepository _accountRepository,
        ITransactionRepository _transactionRepository,
        IUnitOfWork _unitOfWork) 
        
        : ITransferService
    {

        public async Task PerformTransfer(int fromAccountId, int toAccountId, decimal amount)
        {
            Account origen = await _accountRepository.GetByIdAsync(fromAccountId);
            Account destino = await _accountRepository.GetByIdAsync(toAccountId);

            if (origen.Balance < amount)
            {
                throw new InvalidOperationException("Saldo insuficiente en la cuenta de origen.");
            }
            else
            {
                origen.Balance -= amount;
                destino.Balance += amount;

                _accountRepository.Update(origen);
                _accountRepository.Update(destino);

                _transactionRepository.Add(new Transfer { });

                _unitOfWork.SaveChanges();
            }
        }
    }
}
