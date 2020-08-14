using System;
using AutoFixture;
using FluentAssertions;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDDBank.Tests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void Bank_ExportImportTests()
        {
            var b = new Bank();
            b.Customers.Add(new Customer() { Id = 1, Name = "Fred", BirthDate = DateTime.Now });
            b.SaveAll();
            b.LoadAll();
            b.Customers.Count.Should().Be(1);
        }

        [TestMethod]
        public void Bank_ExportImportTests_AutoFix()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            var bank = fix.Create<Bank>();

            var orgCustomers = bank.Customers;

            bank.SaveAll();
            bank.LoadAll();

            bank.Customers.Should().BeEquivalentTo(orgCustomers);

        }

        [TestMethod]
        public void Bank_IsOpen()
        {
            var b = new Bank();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 8, 13, 10, 45, 0);
                b.IsOpen().Should().BeTrue();
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 8, 13, 14, 25, 0);
                b.IsOpen().Should().BeFalse();
            }
        }
    }
}
