using System.Collections.Generic;

namespace WorldWideBank.Models
{
    public class Money : ValueObject
    {
        public double Amount { get; private set; }
        public string Currency { get; private set; }

        private Money() { }

        public Money(double amount, string currency = "CAD")
        {
            Amount = amount;
            Currency = currency;
        }
        
        public override string ToString()
        {
            return $"{Amount:0.00} {Currency}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
