using System;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters
{
    public class ConsoleReporter : IReporter
    {
        public void ReportDownsidePivot(Trade trade)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pivot downside {0}", trade.Date.ToShortDateString());
        }

        public void ReportUpsidePivot(Trade trade)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pivot upside {0}", trade.Date.ToShortDateString());
        }
    }
}
