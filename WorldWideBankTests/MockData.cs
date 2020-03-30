using System.Collections.Generic;
using WorldWideBank.Models;

namespace WorldWideBankTests
{
    public class MockData
    {
        public static IEnumerable<Customer> Customers = new List<Customer>()
        {
            new Customer() {Name = "Stewie Griffin", Id = "777"},
            new Customer() {Name = "Glenn Quagmire", Id = "504"},
            new Customer()
            {
                Name = "Joe Swanson", Id = "002", Accounts = new List<Account>()
                {
                    new Account()
                    {
                        Number = "1010",
                        Balance = new Money(7425)
                    },
                    new Account()
                    {
                        Number = "5500",
                        Balance = new Money(15000)
                    }
                }
            },
            new Customer() {Name = "Peter Griffin", Id = "123"},
            new Customer()
            {
                Name = "Lois Griffin", Id = "456", Accounts = new List<Account>()
                {
                    new Account()
                    {
                        Number = "0456",
                        Balance = new Money(65000)
                    }
                }
            }

        };

    }
}
