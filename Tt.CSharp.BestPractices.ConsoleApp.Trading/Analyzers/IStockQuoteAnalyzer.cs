using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Delegates;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public interface IStockQuotenalyzer
    {
        event PivotDownsideFoundDelegate PivotDownsideFoundEvent;

        event PivotUpsideFoundDelegate PivotUpsideFoundEvent;

        IEnumerable<Reversal> LocateReversal(IList<StockQuote> trades);
    }
}