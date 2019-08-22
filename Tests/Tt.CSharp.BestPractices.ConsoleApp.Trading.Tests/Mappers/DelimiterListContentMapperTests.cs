using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests.Mappers
{
    public class DelimiterListContentMapperTests
    {
        private IContentMapper sut;

        [SetUp]
        public void SetUp()
        {
            sut = new DelimiterListContentMapper();
        }

        [Test]
        public void MapContentsToTrades_Always_ReturnExpectedResult()
        {
            var lines = new[] {
                "Date,Open,High,Low,Close,Volume,Adj Close",
                "9/8/2011,10,11,12,13,14,15",
                "9/7/2011,11,12,13,14,15,16"
            };

            var result = sut.MapContentsToTrades(lines).ToList();

            result.Should().BeEquivalentTo(new List<StockQuote>
            {
                new StockQuote {
                    Date = new DateTime(2011,9,8),
                    Open = 10,
                    High = 11,
                    Low = 12,
                    Close = 13
                },
                new StockQuote {
                    Date = new DateTime(2011,9,7),
                    Open = 11,
                    High = 12,
                    Low = 13,
                    Close = 14
                }
            });
        }
    }
}