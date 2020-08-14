using System;
using System.Collections.Generic;

namespace TDDBank
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public HashSet<BankAccount> Accounts { get; set; } = new HashSet<BankAccount>();
    }

}
