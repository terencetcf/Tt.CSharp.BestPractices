using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters
{
    public class ConsoleReporter : IReporter
    {
        public void Report(IEnumerable<Reversal> reversals)
        {
            foreach (var reversal in reversals)
            {
                Console.ForegroundColor = GetColor(reversal.Direction);
                Console.WriteLine(string.Format(GetMessage(reversal.Direction), reversal.StockQuote.Date.ToShortDateString()));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static ConsoleColor GetColor(ReversalDirection direction)
        {
            return direction == ReversalDirection.Up ? ConsoleColor.Green : ConsoleColor.Red;
        }

        private static string GetMessage(ReversalDirection direction)
        {
            return direction == ReversalDirection.Up ? "Reversed up on {0}" : "Reversed down on {0}";
        }
    }
}
