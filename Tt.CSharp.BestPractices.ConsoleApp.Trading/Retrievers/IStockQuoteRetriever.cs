using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public interface IStockQuoteRetriever
    {
        IEnumerable<StockQuote> GetStockQuotes(string sourcePath);
    }
}