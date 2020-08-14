using System;

namespace TDDBank
{
    public class BankAccount
    {
        public decimal Balance { get;  set; }

        public void Deposit(decimal v)
        {
            if (v < 0) throw new ArgumentException();
            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            Balance -= v;
        }

        public Customer Customer { get; set; }
    }

}
