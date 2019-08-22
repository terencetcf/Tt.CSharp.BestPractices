using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Extensions;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public class ApiStockQuoteRetriever : IStockQuoteRetriever
    {
        private readonly IHttpClientWrapper httpClient;
        private readonly IContentMapper contentMapper;

        public ApiStockQuoteRetriever(IHttpClientWrapper httpClient, IContentMapper contentMapper)
        {
            this.httpClient = httpClient;
            this.contentMapper = contentMapper;
        }

        public IEnumerable<StockQuote> GetStockQuotes(string sourcePath)
        {
            Func<string, Task<string>> download = async url => await GetJsonBlobAsync(url);
            Func<string, Func<Task<string>>> downloadCurry = download.Curry();

            //var jsonBlob = download.Partial(sourcePath).WithRetry().Result;
            var jsonBlob = downloadCurry(sourcePath).WithRetry().Result;

            if (string.IsNullOrWhiteSpace(jsonBlob))
            {
                throw new ArgumentNullException("Source is empty!");
            }

            return contentMapper.MapContentsToTrades(jsonBlob);
        }

        private async Task<string> GetJsonBlobAsync(string requestUrl)
        {
            var response = await httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Unable to retrieve content from api");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}