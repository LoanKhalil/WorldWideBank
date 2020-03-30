using System;
using System.Collections.Generic;
using System.Text;
using WorldWideBank.Models;

namespace WorldWideBank
{
    public static class Converter
    {
        public static Money Convert(this Money from, string to)
        {
            return new Money((@from.Amount * RatesToCanadianDollars[@from.Currency]) * RatesFromCanadianDollars[to], to);
        }


        private static readonly Dictionary<string, double> RatesToCanadianDollars = new Dictionary<string, double>()
        {
            {"CAD", 1},
            {"USD", 2}, 
            {"MXN", 0.1}
        };

        private static readonly Dictionary<string, double> RatesFromCanadianDollars = new Dictionary<string, double>()
        {
            {"CAD", 1},
            {"USD", 0.5},
            {"MXN", 10}
        };

    }
}
