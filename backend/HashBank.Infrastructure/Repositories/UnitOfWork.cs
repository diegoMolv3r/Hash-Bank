using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBank.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public void SaveChanges()
        {
            // Lógica para guardar los cambios en la base de datos
        }
    }
}
