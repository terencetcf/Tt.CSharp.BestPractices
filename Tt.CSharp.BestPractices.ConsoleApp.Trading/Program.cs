using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Factories;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters;
using System.Linq;
using System.Diagnostics;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdArgsRetriever = new CommandLineArgumentsRetriever();
            var sourcePath = cmdArgsRetriever.GetSourcePath(args);
            //sourcePath = "https://jsonblob.com/api/80a3e281-c4fb-11e9-9e37-89e98d519613";

            var factory = new StockQuoteRetrieverFactory();
            var retriever = factory.GetTradeRetriever(sourcePath);
            var trades = retriever.GetStockQuotes(sourcePath).ToList();

            var analyzer = new StockQuoteAnalyzer();
            analyzer.PivotDownsideFoundEvent += PivotDownsideFoundEventHandler;
            analyzer.PivotUpsideFoundEvent += PivotUpsideFoundEventHandler;
            var reversals = analyzer.LocateReversal(trades).AsParallel();
            var reporter = new ConsoleReporter();
            reporter.Report(reversals);

            //var timeAnalyzer = new TimeAnalyzer();
            //var elapsed = timeAnalyzer.Measure(() => PrimeNumberAnalyzer.FindLargePrimesInParallel(900000, 910000));
            //Console.WriteLine(elapsed.TotalMilliseconds);

            //var date = new List<DateTime>();
            //var open = new List<decimal>();
            //var high = new List<decimal>();
            //var low = new List<decimal>();
            //var close = new List<decimal>();

            //var provider = CultureInfo.InvariantCulture;
            //var lines = File.ReadAllLines(args[0]);
            //for (int i = 1; i < lines.Length; i++)
            //{
            //    var data = lines[i].Split(',');
            //    date.Add(DateTime.Parse(data[0], CultureInfo.InvariantCulture));
            //    open.Add(decimal.Parse(data[1]));
            //    high.Add(decimal.Parse(data[2]));
            //    low.Add(decimal.Parse(data[3]));
            //    close.Add(decimal.Parse(data[4]));
            //}

            //for (int i = 0; i < date.Count - 4; i++)
            //{
            //    if (open[i] > high[i + 1] && close[i] < low[i + 1])
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Pivot downside {0}", date[i].ToShortDateString());
            //    }
            //    if (open[i] < low[i + 1] && close[i] > high[i + 1])
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.WriteLine("Pivot upside {0}", date[i].ToShortDateString());
            //    }
            //}
        }

        static void PivotDownsideFoundEventHandler(object sender, DateTime date)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("Pivot downside {0}", date.ToShortDateString());
        }

        static void PivotUpsideFoundEventHandler(object sender, DateTime date)
        {
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Pivot upside {0}", date.ToShortDateString());
        }
    }
}
