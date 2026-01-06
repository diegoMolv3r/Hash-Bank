using HashBank.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HashBank.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string CBU { get; set; } 
        public string Currency { get; set; } = "USD"; 
        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
