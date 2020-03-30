namespace WorldWideBank.Models
{
    public class Account
    {
        public string Number { get; set; }

        public Money Balance { get; set; }

        public Customer Customer { get; set; }

        public Money Deposit(Money amount)
        {
            Balance = new Money(Balance.Amount + amount.Convert("CAD").Amount);
            return Balance;
        }

        public Money Withdraw(Money amount)
        {
            Balance = new Money(Balance.Amount - amount.Convert("CAD").Amount);
            return Balance;
        }

        
        public string GetStatement()
        {
            return $"Account Number: {Number} Balance: {Balance.Amount:C2} {Balance.Currency}";
        }
    }
}
