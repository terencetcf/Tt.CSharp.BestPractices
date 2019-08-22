using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Delegates;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public class StockQuoteAnalyzer : IStockQuotenalyzer
    {
        public event PivotDownsideFoundDelegate PivotDownsideFoundEvent;
        public event PivotUpsideFoundDelegate PivotUpsideFoundEvent;

        public IEnumerable<Reversal> LocateReversal(IList<StockQuote> trades)
        {
            for (int i = 0; i < trades.Count() - 1; i++)
            {
                var current = trades[i];
                var prev = trades[i + 1];

                if (current.ReversesDownFrom(prev))
                {
                    PivotDownsideFoundEvent(this, current.Date);
                    yield return new Reversal(current, ReversalDirection.Down);
                }

                if (current.ReversesUpFrom(prev))
                {
                    PivotUpsideFoundEvent(this, current.Date);
                    yield return new Reversal(current, ReversalDirection.Up);
                }
            }
        }
    }
}