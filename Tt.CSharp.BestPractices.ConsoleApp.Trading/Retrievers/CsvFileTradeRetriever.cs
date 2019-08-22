using System;
using System.Collections.Generic;
using System.Linq;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public class CsvFileTradeRetriever : ITradeRetriever
    {
        private readonly IFileWrapper fileWrapper;
        private readonly IContentMapper contentMapper;

        public CsvFileTradeRetriever(IFileWrapper fileWrapper, IContentMapper contentMapper)
        {
            this.fileWrapper = fileWrapper;
            this.contentMapper = contentMapper;
        }

        public List<Trade> GetTrades(string sourcePath)
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