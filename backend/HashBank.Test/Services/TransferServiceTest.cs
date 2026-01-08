using HashBanck.Services;
using HashBank.Domain.Entities;
using HashBank.Infrastructure.Repositories;
using Moq;

namespace HashBank.Test.Services
{
    public class TransferServiceTest
    {
        [Fact]
        public void Transferir_ConSaldoSuficiente_DeberiaActualizarSaldosYGuardarTransferencia()
        {
            var mockAccountRepo = new Mock<IAccountRepository>();
            var mockTransactionRepo = new Mock<ITransactionRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            Account cuentaOrigen = new() { Id = 1, CBU = "0001", Balance = 1000 };
            Account cuentaDestino = new() { Id = 2, CBU = "0002", Balance = 0 };

            mockAccountRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cuentaOrigen);
            mockAccountRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(cuentaDestino);

            var service = new TransferService(mockAccountRepo.Object, mockTransactionRepo.Object, mockUnitOfWork.Object);

            service.PerformTransfer(1, 2, 500);

            Assert.Equal(500, cuentaOrigen.Balance);
            Assert.Equal(500, cuentaDestino.Balance);

            mockAccountRepo.Verify(r => r.Update(cuentaOrigen), Times.Once);
            mockAccountRepo.Verify(r => r.Update(cuentaDestino), Times.Once);

            mockTransactionRepo.Verify(r => r.Add(It.IsAny<Transfer>()), Times.Once);

            mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }
}