using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests.Mappers
{
    public class JsonContentMapperTests
    {
        private IContentMapper sut;

        [SetUp]
        public void SetUp()
        {
            sut = new JsonContentMapper();
        }

        [Test]
        public void MapContentsToTrades_Always_ReturnExpectedResult()
        {
            var jsonBlob = "[{\"Date\":\"1/2/2019\",\"Open\":1,\"High\":2,\"Low\":1,\"Close\":2},{\"Date\":\"1/1/2019\",\"Open\":3,\"High\":3,\"Low\":3,\"Close\":3}]";

            var result = sut.MapContentsToTrades(jsonBlob);

            result.Should().BeEquivalentTo(
                new List<StockQuote>
                {
                    new StockQuote
                    {
                        Date = new DateTime(2019, 1,2),
                        Open = 1,
                        High = 2,
                        Low = 1,
                        Close = 2
                    },
                    new StockQuote
                    {
                        Date = new DateTime(2019, 1,1),
                        Open = 3,
                        High = 3,
                        Low = 3,
                        Close = 3
                    },
                });
        }
    }
}