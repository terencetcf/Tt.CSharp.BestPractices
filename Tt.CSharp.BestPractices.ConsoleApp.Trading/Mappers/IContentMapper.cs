using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers
{
    public interface IContentMapper
    {
        IEnumerable<StockQuote> MapContentsToTrades(object contents);
    }
}
