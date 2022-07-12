using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp11.Models
{
    public class CurrencyCollection
    {

        private static List<Currency> _Currencies = new List<Currency>
        {
            new Currency {FromCurrency = "USD", ToCurrency = "CAD", Rate = 1.34},
            new Currency {FromCurrency = "CAD ", ToCurrency = "GBP", Rate = 0.58},
            new Currency {FromCurrency = "USD ", ToCurrency = "EUR", Rate = 0.86},
        };

        
        private static List<Currency> updateCurrencies()
        {
            int count = _Currencies.Count;

            for (int i = 0; i < count; i++)
            {
                _Currencies.Add(new Currency
                {
                    ToCurrency = _Currencies[i].FromCurrency,
                    FromCurrency = _Currencies[i].ToCurrency,
                    Rate = (1 / _Currencies[i].Rate)
                });
            }

            return _Currencies;
        }


        protected static List<Currency> initialCurrencies() => updateCurrencies();

        protected double CalculateProportion(double arg1, double arg2) => arg1 / arg2;




    }
}
