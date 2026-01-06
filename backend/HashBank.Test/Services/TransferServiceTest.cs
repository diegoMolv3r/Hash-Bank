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
            var mockRepo = new Mock<IAccountRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            Account cuentaOrigen = new() { Id = 1, CBU = "0001", Balance = 1000 };
            Account cuentaDestino = new() { Id = 2, CBU = "0002", Balance = 0 };

            mockRepo.Setup(r => r.GetById(1)).Returns(cuentaOrigen);
            mockRepo.Setup(r => r.GetById(2)).Returns(cuentaDestino);

            var service = new TransferService(mockRepo.Object, mockUnitOfWork.Object);

            service.PerformTransfer(1, 2, 500);

            Assert.Equal(500, cuentaOrigen.Balance);
            Assert.Equal(500, cuentaDestino.Balance);

            mockRepo.Verify(r => r.Update(cuentaOrigen), Times.Once);
            mockRepo.Verify(r => r.Update(cuentaDestino), Times.Once);

            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transfer>()), Times.Once);

            mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }
}