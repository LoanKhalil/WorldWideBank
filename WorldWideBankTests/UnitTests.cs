using System.Linq;
using NUnit.Framework;
using WorldWideBank;
using WorldWideBank.Models;

namespace WorldWideBankTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, "CAD", "USD", ExpectedResult = 0.5)]
        [TestCase(0.5, "USD", "CAD", ExpectedResult = 1)]
        [TestCase(10, "MXN", "CAD", ExpectedResult = 1)]
        [TestCase(10, "MXN", "USD", ExpectedResult = 0.5)]
        public double TestConversions(double amount, string from, string to)
        {
            var balance = new Money(amount, from);
            return balance.Convert(to).Amount;
        }

        [Test]
        public void Case1()
        {
            //arrange
            var account = new Account()
            {
                Number = "1234",
                Customer = MockData.Customers.FirstOrDefault(x => x.Id.Equals("777")),
                Balance = new Money(100)
            };

            //act
            var result = account.Deposit(new Money(300, "USD"));

            //assert
            Assert.AreEqual("Account Number: 1234 Balance: $700.00 CAD", account.GetStatement());
        }

        [Test]
        public void Case2()
        {
            //arrange
            var account = new Account()
            {
                Number = "2001",
                Customer = MockData.Customers.FirstOrDefault(x => x.Id.Equals("504")),
                Balance = new Money(35000)
            };

            //act
            account.Withdraw(new Money(5000, "MXN"));
            account.Withdraw(new Money(12500, "USD"));
            account.Deposit(new Money(300));

            //assert
            Assert.AreEqual("Account Number: 2001 Balance: $9,800.00 CAD", account.GetStatement());
        }

        [Test]
        public void Case3()
        {
            //arrange
            Customer joe = MockData.Customers.First(x => x.Id.Equals("002"));

            Account account1010 = joe.Accounts.First(x => x.Number.Equals("1010"));
            Account account5500 = joe.Accounts.First(x => x.Number.Equals("5500"));

            //act
            account5500.Withdraw(new Money(5000));
            joe.Transfer("1010", "5500", new Money(7300));
            account1010.Deposit(new Money(13726, "MXN"));

            //assert
            Assert.AreEqual("Account Number: 1010 Balance: $1,497.60 CAD", account1010.GetStatement());
            Assert.AreEqual("Account Number: 5500 Balance: $17,300.00 CAD", account5500.GetStatement());
        }

        [Test]
        public void Case4()
        {
            //arrange
            var account0123 = new Account()
            {
                Number = "0123",
                Customer = MockData.Customers.First(x => x.Id.Equals("123")),
                Balance = new Money(150)
            };
            Customer lois = MockData.Customers.First(x => x.Id.Equals("456"));
            var account0456 = lois.Accounts.First(x => x.Number.Equals("0456"));


            //act
            account0123.Withdraw(new Money(70, "USD"));
            account0456.Deposit(new Money(23789, "USD"));
            lois.Transfer("0456", account0123, new Money(23.75));

            //assert
            Assert.AreEqual("Account Number: 0123 Balance: $33.75 CAD", account0123.GetStatement());
            Assert.AreEqual("Account Number: 0456 Balance: $112,554.25 CAD", account0456.GetStatement());
        }
    }
}