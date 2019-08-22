using System;
using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public class ApiTradeRetriever : ITradeRetriever
    {
        private readonly IHttpClientWrapper httpClient;
        private readonly IContentMapper contentMapper;

        public ApiTradeRetriever(IHttpClientWrapper httpClient, IContentMapper contentMapper)
        {
            this.httpClient = httpClient;
            this.contentMapper = contentMapper;
        }

        public List<Trade> GetTrades(string sourcePath)
        {
            var response = httpClient.GetAsync(sourcePath).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Unable to retrieve content from api");
            }

            var jsonBlob = response.Content.ReadAsStringAsync().Result;

            return contentMapper.MapContentsToTrades(jsonBlob).ToList();
        }
    }
}