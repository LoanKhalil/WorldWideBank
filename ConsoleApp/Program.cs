using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldWideBank;
using WorldWideBank.Models;

namespace ConsoleApp
{
    class Program
    {
        

        static async Task Main(string[] args)
        {
            var dbContext = new WorldWideBankContext();
            await dbContext.Database.MigrateAsync().ConfigureAwait(false);

            if (!await dbContext.Customers.AnyAsync() && !await dbContext.Accounts.AnyAsync())
            {
                Customer c1 = new Customer()
                {
                    Id = "777",
                    Name = "Stewie Griffin",
                    Accounts = new List<Account>()
                    {
                        new Account() {Number = "1234", Balance = new Money(100)}
                    }
                };
                Customer c2 = new Customer()
                {
                    Id = "504",
                    Name = "Glenn Quagmire",
                    Accounts = new List<Account>()
                    {
                        new Account() {Number = "2001", Balance = new Money(35000)}
                    }
                };
                Customer c3 = new Customer()
                {
                    Id = "002",
                    Name = "Joe Swanson",
                    Accounts = new List<Account>()
                    {
                        new Account() {Number = "1010", Balance = new Money(7425)},
                        new Account() {Number = "5500", Balance = new Money(15000)}
                    }
                };
                Customer c4 = new Customer()
                {
                    Id = "123",
                    Name = "Peter Griffin",
                    Accounts = new List<Account>()
                    {
                        new Account() {Number = "0123", Balance = new Money(150)}
                    }
                };
                Customer c5 = new Customer()
                {
                    Id = "456",
                    Name = "Lois Griffin",
                    Accounts = new List<Account>()
                    {
                        new Account() {Number = "0456", Balance = new Money(65000)}
                    }
                };

                dbContext.Customers.AddRange(c1, c2, c3, c4, c5);
                await dbContext.SaveChangesAsync();

            }

            Console.WriteLine("******************************************");
            Console.WriteLine("*    Initialize Database                 *");
            Console.WriteLine("******************************************");

            foreach (Customer customer in dbContext.Customers.Include(x => x.Accounts))
            {
                Console.WriteLine($"Customer: {customer.Name}");
                Console.WriteLine($"Customer ID: {customer.Id}");

                foreach (Account account in customer.Accounts)
                {
                    Console.WriteLine($"Account Number: {account.Number}");   
                    Console.WriteLine($"Initial balance for account number {account.Number}: {account.Balance.Amount:C2} {account.Balance.Currency}");
                }

                Console.WriteLine("----------------------------------------------");
            }

            Console.WriteLine("******************************************");
            Console.WriteLine("*      BANK TRANSACTIONS                 *");
            Console.WriteLine("******************************************");

            Console.WriteLine("");
            Console.WriteLine("Case 1:");
            Console.WriteLine("Transaction(s):");
            Console.WriteLine("Stewie Griffin deposits $300.00 USD to account number 1234.");

            Customer stewie = dbContext.Customers.Include(x => x.Accounts).Single(x => x.Id.Equals("777"));
            Account account1234 = stewie.Accounts.Single(x => x.Number.Equals("1234"));
            account1234.Deposit(new Money(300, "USD"));
            Console.WriteLine(account1234.GetStatement());

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("");
            Console.WriteLine("Case 2:");
            Console.WriteLine("Transaction(s):");
            Console.WriteLine("Glenn Quagmire withdraws $5,000.00 MXN from account number 2001.");
            Console.WriteLine("Glenn Quagmire withdraws $12,500.00 USD from account number 2001.");
            Console.WriteLine("Glenn Quagmire deposits $300.00 CAD to account number 2001.");

            Customer glenn = dbContext.Customers.Include(x => x.Accounts).Single(x => x.Id.Equals("504"));
            Account account2001 = glenn.Accounts.Single(x => x.Number.Equals("2001"));
            account2001.Withdraw(new Money(5000, "MXN"));
            account2001.Withdraw(new Money(12500, "USD"));
            account2001.Deposit(new Money(300, "CAD"));
            Console.WriteLine(account2001.GetStatement());

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("");
            Console.WriteLine("Case 3:");
            Console.WriteLine("Transaction(s):");
            Console.WriteLine("Joe Swanson withdraws $5,000.00 CAD from account number 5500.");
            Console.WriteLine("Joe Swanson transfers $7,300.00 CAD from account number 1010 to account number 5500.");
            Console.WriteLine("Joe Swanson deposits $13,726.00 MXN to account number 1010.");

            Customer joe = dbContext.Customers.Include(x => x.Accounts).Single(x => x.Id.Equals("002"));
            Account account1010 = joe.Accounts.Single(x => x.Number.Equals("1010"));
            Account account5500 = joe.Accounts.Single(x => x.Number.Equals("5500"));

            account5500.Withdraw(new Money(5000, "CAD"));
            joe.Transfer("1010", "5500", new Money(7300));
            account1010.Deposit(new Money(13726, "MXN"));
            Console.WriteLine(account1010.GetStatement());
            Console.WriteLine(account5500.GetStatement());


            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("");
            Console.WriteLine("Case 4:");
            Console.WriteLine("Transaction(s):");
            Console.WriteLine("Peter Griffin withdraws $70.00 USD from account number 0123.");
            Console.WriteLine("Lois Griffin deposits $23,789.00 USD to account number 0456.");
            Console.WriteLine("Lois Griffin transfers $23.75 CAD from account number 0456 to Peter Griffin (account number 0123).");

            Customer peter = dbContext.Customers.Include(x => x.Accounts).Single(x => x.Id.Equals("123"));
            Account account0123 = peter.Accounts.Single(x => x.Number.Equals("0123"));
            Customer lois = dbContext.Customers.Include(x => x.Accounts).Single(x => x.Id.Equals("456"));
            Account account0456 = lois.Accounts.Single(x => x.Number.Equals("0456"));

            account0123.Withdraw(new Money(70, "USD"));
            account0456.Deposit(new Money(23789, "USD"));
            lois.Transfer("0456", account0123, new Money(23.75, "CAD"));
            
            Console.WriteLine(account0123.GetStatement());
            Console.WriteLine(account0456.GetStatement());

        }
    }
}
