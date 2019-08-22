using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Reporters;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests
{
    public class TradeAnalyzerTests
    {
        private ITradeAnalyzer sut;

        private Mock<IReporter> mockReporter;
        private List<Trade> trades;
        private List<string>  result;

        [SetUp]
        public void SetUp()
        {
            result = new List<string>();
            trades = new List<Trade>
            {
                new Trade { Date = new DateTime(2019, 9, 8), Open = 12, High = 13, Low = 10, Close = 11 },
                new Trade { Date = new DateTime(2019, 9, 7), Open = 11, High = 10, Low = 12, Close = 14 },
                new Trade { Date = new DateTime(2019, 9, 6), Open = 16, High = 13, Low = 12, Close = 14 },
                new Trade { Date = new DateTime(2019, 9, 5), Open = 12, High = 10, Low = 15, Close = 11 },
                new Trade { Date = new DateTime(2019, 9, 4), Open = 11, High = 10, Low = 10, Close = 14 },
                new Trade { Date = new DateTime(2019, 9, 3), Open = 11, High = 10, Low = 10, Close = 14 },
                new Trade { Date = new DateTime(2019, 9, 2), Open = 11, High = 10, Low = 10, Close = 14 },
                new Trade { Date = new DateTime(2019, 9, 1), Open = 11, High = 10, Low = 10, Close = 14 },
            };

            mockReporter = new Mock<IReporter>();
            mockReporter.Setup(s => s.ReportUpsidePivot(It.IsAny<Trade>()));
            mockReporter.Setup(s => s.ReportDownsidePivot(It.IsAny<Trade>()));

            sut = new TradeAnalyzer(mockReporter.Object);
            sut.PivotDownsideFoundEvent += PivotDownsideFoundEventHandler;
            sut.PivotUpsideFoundEvent += PivotUpsideFoundEventHandler;
        }

        [Test]
        public void TestMethod_Always_ReturnExpectedResult()
        {
            sut.AnalyzeTrades(trades);

            result.Should().BeEquivalentTo(
               new List<string>
               {
                  "PivotDown:08/09/2019",
                  "PivotUp:07/09/2019",
                  "PivotDown:06/09/2019",
               }, option => option.WithStrictOrdering());
            mockReporter.Verify(s => s.ReportUpsidePivot(It.IsAny<Trade>()), Times.Once);
            mockReporter.Verify(s => s.ReportDownsidePivot(It.IsAny<Trade>()), Times.Exactly(2));
        }

        private void PivotDownsideFoundEventHandler(object sender, DateTime date)
        {
            result.Add($"PivotDown:{date.ToShortDateString()}");
        }

        private void PivotUpsideFoundEventHandler(object sender, DateTime date)
        {
            result.Add($"PivotUp:{date.ToShortDateString()}");
        }
    }
}