using HashBank.Domain.Entities;
using HashBank.Infrastructure.Repositories;

namespace HashBanck.Services
{
    public interface ITransferService
    {
        public void PerformTransfer(int fromAccountId, int toAccountId, decimal amount);
    }
    public class TransferService(IAccountRepository _accountRepository, IUnitOfWork _unitOfWork) : ITransferService
    {

        public void PerformTransfer(int fromAccountId, int toAccountId, decimal amount)
        {
            Account origen = _accountRepository.GetById(fromAccountId);
            Account destino = _accountRepository.GetById(toAccountId);

            if (origen.Balance < amount)
            {
                throw new InvalidOperationException("Saldo insuficiente en la cuenta de origen.");
            }

            origen.Balance -= amount;
            destino.Balance += amount;

            _accountRepository.Update(origen);
            _accountRepository.Update(destino);

            _accountRepository.AddTransaction(new Transfer {});

            _unitOfWork.SaveChanges();
        }
    }
}
