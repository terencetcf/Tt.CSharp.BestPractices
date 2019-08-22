using System;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities
{
    public class Trade
    {
        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }
    }
}
