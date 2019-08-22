using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public interface ITradeRetriever
    {
        List<Trade> GetTrades(string sourcePath);
    }
}