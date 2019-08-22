using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.TestsTests.Readers
{
    public class CommandLineArgumentsRetrieverTests
    {
        private ICommandLineArgumentsRetriever sut;

        [SetUp]
        public void SetUp()
        {
            sut = new CommandLineArgumentsRetriever();
        }

        [TestCaseSource(nameof(InvalidArgumentsTestCaseSource))]
        public void GetSourcePath_IfInvalidArguments_ThrowException(string[] args)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetSourcePath(null));
        }

        [Test]
        public void GetSourcePath_Always_ReturnExpectedResult()
        {
            const string sourcePath = "aPath";
            var result = sut.GetSourcePath(new[] { sourcePath });

            Assert.AreEqual(sourcePath, result);
        }

        private static IEnumerable<string[]> InvalidArgumentsTestCaseSource()
        {
            yield return null;
            yield return new string[] { };
            yield return new string[] { "  " };
        }
    }
}
