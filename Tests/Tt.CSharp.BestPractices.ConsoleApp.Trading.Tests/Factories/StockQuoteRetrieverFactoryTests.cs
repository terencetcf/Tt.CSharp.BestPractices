using NUnit.Framework;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Factories;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Tests.Factories
{
    public class StockQuoteRetrieverFactoryTests
    {
        private IStockQuoteRetrieverFactory sut;

        [SetUp]
        public void SetUp()
        {
            sut = new StockQuoteRetrieverFactory();
        }

        [Test]
        public void GetTradeRetriever_IfSourcePathIsHttp_ReturnExpectedResult()
        {
            var result = sut.GetTradeRetriever("http://test.api");

            Assert.IsInstanceOf<ApiStockQuoteRetriever>(result);
        }

        [Test]
        public void GetTradeRetriever_IfSourcePathIsOther_ReturnExpectedResult()
        {
            var result = sut.GetTradeRetriever("test.csv");

            Assert.IsInstanceOf<CsvFileStockQuoteRetriever>(result);
        }
    }
}
