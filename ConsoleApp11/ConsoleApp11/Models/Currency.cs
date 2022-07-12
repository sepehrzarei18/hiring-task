using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Models
{
    public class Currency
    {
        public string FromCurrency
        {
            get; set;
        }

        public string ToCurrency { get; set; }
 
        public double Rate { get; set; }


        public bool Compare(string fromCurrency , string toCurrency) => this.FromCurrency.ToString() == fromCurrency.ToString() && this.ToCurrency.ToString() == toCurrency.ToString();
        

    }
}
