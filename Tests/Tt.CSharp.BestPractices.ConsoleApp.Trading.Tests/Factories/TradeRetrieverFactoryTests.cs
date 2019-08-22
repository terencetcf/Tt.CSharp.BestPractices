using NUnit.Framework;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Factories;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;

namespace Tt.CSharp.BestPractices.TradeAnalyzerTests.Factories
{
    public class TradeRetrieverFactoryTests
    {
        private ITradeRetrieverFactory sut;

        [SetUp]
        public void SetUp()
        {
            sut = new TradeRetrieverFactory();
        }

        [Test]
        public void GetTradeRetriever_IfSourcePathIsHttp_ReturnExpectedResult()
        {
            var result = sut.GetTradeRetriever("http://test.api");

            Assert.IsInstanceOf<ApiTradeRetriever>(result);
        }

        [Test]
        public void GetTradeRetriever_IfSourcePathIsOther_ReturnExpectedResult()
        {
            var result = sut.GetTradeRetriever("test.csv");

            Assert.IsInstanceOf<CsvFileTradeRetriever>(result);
        }
    }
}
