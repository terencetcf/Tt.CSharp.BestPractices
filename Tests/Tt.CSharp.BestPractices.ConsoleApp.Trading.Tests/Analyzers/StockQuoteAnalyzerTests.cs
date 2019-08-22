using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests.Analyzers
{
    public class StockQuoteAnalyzerTests
    {
        private IStockQuotenalyzer sut;

        private List<StockQuote> stockQuotes;
        private List<string>  resultFromEvents;

        [SetUp]
        public void SetUp()
        {
            resultFromEvents = new List<string>();
            stockQuotes = new List<StockQuote>
            {
                new StockQuote { Date = new DateTime(2019, 9, 8), Open = 12, High = 13, Low = 10, Close = 11 },
                new StockQuote { Date = new DateTime(2019, 9, 7), Open = 11, High = 10, Low = 12, Close = 14 },
                new StockQuote { Date = new DateTime(2019, 9, 6), Open = 16, High = 13, Low = 12, Close = 14 },
                new StockQuote { Date = new DateTime(2019, 9, 5), Open = 12, High = 10, Low = 15, Close = 11 },
                new StockQuote { Date = new DateTime(2019, 9, 4), Open = 11, High = 10, Low = 10, Close = 14 },
                new StockQuote { Date = new DateTime(2019, 9, 3), Open = 11, High = 10, Low = 10, Close = 14 },
                new StockQuote { Date = new DateTime(2019, 9, 2), Open = 11, High = 10, Low = 10, Close = 14 },
                new StockQuote { Date = new DateTime(2019, 9, 1), Open = 11, High = 10, Low = 10, Close = 14 },
            };

            sut = new StockQuoteAnalyzer();
            sut.PivotDownsideFoundEvent += PivotDownsideFoundEventHandler;
            sut.PivotUpsideFoundEvent += PivotUpsideFoundEventHandler;
        }

        [Test]
        public void LocateReversal_Always_ReturnExpectedResult()
        {
            var result = sut.LocateReversal(stockQuotes).ToList();

            result.Should().BeEquivalentTo(
                new List<Reversal>
                {
                    new Reversal(stockQuotes[0], ReversalDirection.Down),
                    new Reversal(stockQuotes[1], ReversalDirection.Up),
                    new Reversal(stockQuotes[2], ReversalDirection.Down),
                });
            resultFromEvents.Should().BeEquivalentTo(
               new List<string>
               {
                  "PivotDown:08/09/2019",
                  "PivotUp:07/09/2019",
                  "PivotDown:06/09/2019",
               }, option => option.WithStrictOrdering());
        }

        private void PivotDownsideFoundEventHandler(object sender, DateTime date)
        {
            resultFromEvents.Add($"PivotDown:{date.ToShortDateString()}");
        }

        private void PivotUpsideFoundEventHandler(object sender, DateTime date)
        {
            resultFromEvents.Add($"PivotUp:{date.ToShortDateString()}");
        }
    }
}