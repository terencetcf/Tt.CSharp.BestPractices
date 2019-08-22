using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters
{
    public interface IReporter
    {
        void Report(IEnumerable<Reversal> reversals);
    }
}
