using System;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters
{
    public interface IReporter
    {
        void ReportDownsidePivot(Trade trade);

        void ReportUpsidePivot(Trade trade);
    }
}
