using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp1.Models;
using ConsoleApp11.Models;

namespace ConsoleApp11.Services
{
    public interface ICurrencyConverter
    {
        double Convert(string fromCurrency = "", string toCurrency = "", double amount = 0);
        void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);
        void ClearConfiguration();
    }

    public class CurrencyConverter : CurrencyCollection, ICurrencyConverter
    {
        private List<Currency> _currencies;
        private IEnumerable<Tuple<string, string, double>> _ratesConversion;

        public CurrencyConverter()
        {
            _currencies = initialCurrencies();
            _ratesConversion = null;
        }

        /// <summary>
        ///  Clears any prior configuration. 
        /// </summary>
        public void ClearConfiguration()
        {
            _ratesConversion.ToList().ForEach(item =>
            {
                var findRecord = _currencies.SingleOrDefault(c => c.FromCurrency.ToString() == item.Item1.ToString()
                                                                  && c.ToCurrency == item.Item2.ToString());

                if (findRecord != null)
                    _currencies.Remove(findRecord);
            });

        }

        /// <summary>
        /// The job of this method is to first check if there is a converted currency in my created list or not, if there is, it converts the amount, otherwise it will find a way to find the correct conversion. 
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public double Convert(string fromCurrency = "", string toCurrency = "", double amount = 0)
        {
            var findFirstTryInList = _currencies.SingleOrDefault(
                c => c.FromCurrency.ToString() == fromCurrency.ToUpper().ToString()
                && c.ToCurrency.ToString() == toCurrency.ToUpper().ToString());

            double calculateResult = Double.NaN;

            
            if (findFirstTryInList != null)
            {
                calculateResult = findFirstTryInList.Rate * amount;
            }
            else
            {
                
                var getFromCurrencyConvertedToCurrencies = _currencies.Where(c => c.FromCurrency.ToString() == fromCurrency)
                    .ToList();

                Currency currencyFind = new Currency();

                foreach (var fromCurrencyItm in getFromCurrencyConvertedToCurrencies)
                {
                    foreach (var toCurrencyItm in _currencies)
                    {
                        if (fromCurrencyItm.ToCurrency == toCurrencyItm.ToCurrency)
                        {
                            currencyFind = toCurrencyItm;
                            break;
                        }
                    }
                }

                if (currencyFind != null)
                {
                    var firstConvert = amount * currencyFind.Rate;

                    foreach (var currency in _currencies)
                    {
                        if (currency.Compare(currency.FromCurrency, toCurrency))
                        {
                            calculateResult = firstConvert * currency.Rate;

                            var newCurrencyObject = new Currency
                            {
                                FromCurrency = fromCurrency,
                                ToCurrency = toCurrency,
                                Rate = CalculateProportion(currency.Rate , currencyFind.Rate)
                            };

                            _ratesConversion = new List<Tuple<string, string, double>>
                            {
                                new Tuple<string, string, double>(newCurrencyObject.FromCurrency.ToString(), newCurrencyObject.ToCurrency.ToString(), newCurrencyObject.Rate)
                            };

                            UpdateConfiguration(_ratesConversion);

                            break;
                        }
                    }
                }
            }


            return calculateResult;
        }

        /// <summary>
        /// Updates the configuration. Rates are inserted or replaced internally. 
        /// </summary>
        /// <param name="conversionRates"></param>
        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            foreach (var item in conversionRates)
            {
                var newCurrency = new Currency
                {
                    FromCurrency = item.Item1,
                    ToCurrency = item.Item2,
                    Rate = item.Item3
                };

                _currencies.Add(newCurrency);
            }
        }
    }
}
