using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDDBank.Tests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        [Description("Mehr tollen der genau beschreibt was hier passiert")]
        public void BankAccount_new_account_should_have_0_as_balance()
        {
            //Arrange
            BankAccount ba;
            //Act
            ba = new BankAccount();

            //Assert
            Assert.AreEqual(0m, ba.Balance);
        }

        [TestMethod]
        public void BankAccount_deposit()
        {
            var ba = new BankAccount();

            ba.Deposit(5m);

            Assert.AreEqual(5m, ba.Balance);

            ba.Deposit(7m);

            Assert.AreEqual(12m, ba.Balance);
        }

        [TestMethod]
        public void BankAccount_deposit_should_throw_on_negative_value()
        {
            var ba = new BankAccount();

            Assert.ThrowsException<ArgumentException>(() => ba.Deposit(-5m));

        }


        [TestMethod]
        public void BankAccount_with()
        {
            var ba = new BankAccount();

            ba.Deposit(15m);
            ba.Withdraw(7m);

            ba.Balance.Should().Be(8m);
            ba.Balance.Should().BeInRange(4, 12);
            
        }

    }
}
