using System;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Factories
{
    public interface ITradeRetrieverFactory
    {
        ITradeRetriever GetTradeRetriever(string sourcePath);
    }

    public class TradeRetrieverFactory : ITradeRetrieverFactory
    {
        public ITradeRetriever GetTradeRetriever(string sourcePath)
        {
            if (sourcePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return new ApiTradeRetriever(new HttpClientWrapper(), new JsonContentMapper());
            }

            return new CsvFileTradeRetriever(new FileWrapper(), new DelimiterListContentMapper());
        }
    }
}
