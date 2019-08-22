using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests.Retrievers
{
    public class CsvFileStockQuoteRetrieverTests
    {
        private IStockQuoteRetriever sut;

        private const string sourcePath = "test.csv";
        private Mock<IFileWrapper> mockFileWrapper;

        [SetUp]
        public void SetUp()
        {
            mockFileWrapper = new Mock<IFileWrapper>();

            sut = new CsvFileStockQuoteRetriever(mockFileWrapper.Object, new DelimiterListContentMapper());
        }

        [Test]
        public void ReadContents_IfFileDoesNotExists_ThrowException()
        {
            mockFileWrapper.Setup(s => s.Exists(sourcePath)).Returns(false);

            Assert.Throws<InvalidOperationException>(() => sut.GetStockQuotes(sourcePath));
        }

        [Test]
        public void ReadContents_Always_ReturnExpectedResult()
        {
            var response = new[] {
                "Date,Open,High,Low,Close,Volume,Adj Close",
                "9/8/2011,10,11,12,13,14,15",
                "9/7/2011,11,12,13,14,15,16"
            };
            mockFileWrapper.Setup(s => s.Exists(sourcePath)).Returns(true);
            mockFileWrapper.Setup(s => s.ReadAllLines(sourcePath)).Returns(response);

            var result = sut.GetStockQuotes(sourcePath);

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
