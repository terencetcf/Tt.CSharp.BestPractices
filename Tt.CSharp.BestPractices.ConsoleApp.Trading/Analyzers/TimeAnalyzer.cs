using System;
using System.Diagnostics;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public class TimeAnalyzer
    {
        public TimeSpan Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            watch.Stop();

            return watch.Elapsed;
        }
    }
}
