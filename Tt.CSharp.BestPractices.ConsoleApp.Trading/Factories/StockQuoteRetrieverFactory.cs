using System;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Factories
{
    public interface IStockQuoteRetrieverFactory
    {
        IStockQuoteRetriever GetTradeRetriever(string sourcePath);
    }

    public class StockQuoteRetrieverFactory : IStockQuoteRetrieverFactory
    {
        public IStockQuoteRetriever GetTradeRetriever(string sourcePath)
        {
            if (sourcePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return new ApiStockQuoteRetriever(new HttpClientWrapper(), new JsonContentMapper());
            }

            return new CsvFileStockQuoteRetriever(new FileWrapper(), new DelimiterListContentMapper());
        }
    }
}
