using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWideBank.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public bool Transfer(string from, string to, Money amount)
        {
            var fromAccount = Accounts.FirstOrDefault(x => x.Number == from);
            if (fromAccount == null) return false;

            var toAccount = Accounts.FirstOrDefault(x => x.Number == to);
            if (toAccount == null) return false;

            var canadianFund = amount.Convert("CAD");

            fromAccount.Withdraw(canadianFund);
            toAccount.Deposit(canadianFund);
            return true;
        }

        public bool Transfer(string from, Account to, Money amount)
        {
            var fromAccount = Accounts.FirstOrDefault(x => x.Number == from);
            if (fromAccount == null) return false;

            var canadianFund = amount.Convert("CAD");
            fromAccount.Withdraw(canadianFund);
            to.Deposit(canadianFund);

            return true;
        }
    }
}
