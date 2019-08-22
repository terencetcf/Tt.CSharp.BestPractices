using System;
using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public class CsvFileStockQuoteRetriever : IStockQuoteRetriever
    {
        private readonly IFileWrapper fileWrapper;
        private readonly IContentMapper contentMapper;

        public CsvFileStockQuoteRetriever(IFileWrapper fileWrapper, IContentMapper contentMapper)
        {
            this.fileWrapper = fileWrapper;
            this.contentMapper = contentMapper;
        }

        public IEnumerable<StockQuote> GetStockQuotes(string sourcePath)
        {
            if (!fileWrapper.Exists(sourcePath))
            {
                throw new InvalidOperationException("File does not exist");
            }

            var lines =  fileWrapper.ReadAllLines(sourcePath);

            return contentMapper.MapContentsToTrades(lines).ToList();
        }
    }
}