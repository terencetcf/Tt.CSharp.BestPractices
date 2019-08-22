using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Mappers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Wrappers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.TestsTests
{
    public class ApiTradeRetrieverTests
    {
        private ITradeRetriever sut;

        private const string SourcePath = "http://the.api/jsonblob";
        private Mock<IHttpClientWrapper> mockHttpClient;

        [SetUp]
        public void SetUp()
        {
            mockHttpClient = new Mock<IHttpClientWrapper>();
            sut = new ApiTradeRetriever(mockHttpClient.Object, new JsonContentMapper());
        }

        [Test]
        public void GetTrades_IfNotSuccessStatusCode_ThrowException()
        {
            mockHttpClient.Setup(s => s.GetAsync(SourcePath))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            Assert.Throws<InvalidOperationException>(() => sut.GetTrades(SourcePath));
        }

        [Test]
        public void GetTrades_Always_ReturnExpectedResult()
        {
            mockHttpClient.Setup(s => s.GetAsync(SourcePath))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"Date\":\"1/2/2019\",\"Open\":1,\"High\":2,\"Low\":1,\"Close\":2},{\"Date\":\"1/1/2019\",\"Open\":3,\"High\":3,\"Low\":3,\"Close\":3}]")
                });

            var result = sut.GetTrades(SourcePath);

            result.Should().BeEquivalentTo(
                new List<Trade>
                {
                    new Trade
                    {
                        Date = new DateTime(2019, 1,2),
                        Open = 1,
                        High = 2,
                        Low = 1,
                        Close = 2
                    },
                    new Trade
                    {
                        Date = new DateTime(2019, 1,1),
                        Open = 3,
                        High = 3,
                        Low = 3,
                        Close = 3
                    }
                });
        }
    }
}
