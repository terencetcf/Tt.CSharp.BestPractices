using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Delegates;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public class TradeAnalyzer : ITradeAnalyzer
    {
        private readonly IReporter reporter;

        public TradeAnalyzer(IReporter reporter)
        {
            this.reporter = reporter;
        }

        public event PivotDownsideFoundDelegate PivotDownsideFoundEvent;
        public event PivotUpsideFoundDelegate PivotUpsideFoundEvent;

        public void AnalyzeTrades(IList<Trade> trades)
        {
            for (int i = 0; i < trades.Count() - 4; i++)
            {
                var current = trades[i];
                var prev = trades[i + 1];
                if (IsPivotDownside(current, prev))
                {
                    reporter.ReportDownsidePivot(current);
                    PivotDownsideFoundEvent(this, current.Date);
                }

                if (IsPivotUpside(current, prev))
                {
                    reporter.ReportUpsidePivot(current);
                    PivotUpsideFoundEvent(this, current.Date);
                }
            }
        }

        private static bool IsPivotDownside(Trade current, Trade prev)
        {
            return current.Open > prev.High && current.Close < prev.Low;
        }

        private static bool IsPivotUpside(Trade current, Trade prev)
        {
            return current.Open < prev.Low && current.Close > prev.High;
        }
    }
}