using HashBank.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HashBank.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Propiedad de Navegación (Relación 1 a N: Un usuario tiene muchas cuentas)
        public ICollection<Account> Accounts { get; set; }
    }
}
