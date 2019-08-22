using System;
using System.Linq;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Retrievers
{
    public interface ICommandLineArgumentsRetriever
    {
        string GetSourcePath(string[] commandlineArgs);
    }

    public class CommandLineArgumentsRetriever : ICommandLineArgumentsRetriever
    {
        public string GetSourcePath(string[] commandlineArgs)
        {
            var sourcePath = commandlineArgs?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentNullException("Please supply source path!");
            }

            return sourcePath;
        }
    }
}