using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Delegates;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public interface ITradeAnalyzer
    {
        event PivotDownsideFoundDelegate PivotDownsideFoundEvent;

        event PivotUpsideFoundDelegate PivotUpsideFoundEvent;

        void AnalyzeTrades(IList<Trade> trades);
    }
}