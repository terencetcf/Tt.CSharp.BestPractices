using Newtonsoft.Json;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers
{
    public class JsonContentMapper : IContentMapper
    {
        public IEnumerable<Trade> MapContentsToTrades(object contents)
        {
            var jsonBlob = contents as string;
            var trades = JsonConvert.DeserializeObject<IEnumerable<Trade>>(jsonBlob);

            return trades;
        }
    }
}
