using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using ConsoleApp11.Models;
using ConsoleApp11.Services;

namespace ConsoleApp11
{
    class Program
    {
        private static ICurrencyConverter _currencyConverter;
        static void Main(string[] args)
        {
            _currencyConverter = new CurrencyConverter();

            bool isProgramAlive = true;

            do
            {
                IEnumerable<Tuple<string, string, double>> currencyList = new List<Tuple<string, string, double>>()
                {
                    new Tuple<string, string, double>("GBP","USD",10),
                    new Tuple<string, string, double>("CAD", "EUR", 10),
                    new Tuple<string, string, double>("USD", "GBP", 10),
                    new Tuple<string, string, double>("CAD", "GBP", 10),
                    new Tuple<string, string, double>("CAD", "USD", 10),

                };

                foreach (var item in currencyList)
                {
                    var convert = _currencyConverter.Convert(item.Item1, item.Item2, item.Item3);
                    Console.WriteLine(convert);
                }

                _currencyConverter.ClearConfiguration();

                Console.ReadKey();

            } while (isProgramAlive);

        }
    }
}
