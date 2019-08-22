using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers
{
    public class DelimiterListContentMapper : IContentMapper
    {
        public IEnumerable<StockQuote> MapContentsToTrades(object contents)
        {
            var lines = contents as string[];

            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(',');
                yield return new StockQuote()
                {
                    Date = DateTime.Parse(columns[0], CultureInfo.InvariantCulture),
                    Open = decimal.Parse(columns[1]),
                    High = decimal.Parse(columns[2]),
                    Low = decimal.Parse(columns[3]),
                    Close = decimal.Parse(columns[4])
                };
            }
        }
    }
}
