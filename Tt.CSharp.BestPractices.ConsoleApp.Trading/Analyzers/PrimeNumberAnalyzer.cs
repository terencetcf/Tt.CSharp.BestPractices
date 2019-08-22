using System.Collections.Generic;
using System.Linq;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Analyzers
{
    public static class PrimeNumberAnalyzer
    {
        public static IList<int> FindLargePrimes(int start, int end)
        {
            var primes = Enumerable.Range(start, end - start).ToList();

            return primes.Where(IsPrime).ToList();
        }

        public static IList<int> FindLargePrimesInParallel(int start, int end)
        {
            var primes = Enumerable.Range(start, end - start).ToList();

            return primes.AsParallel().Where(IsPrime).ToList();
        }

        private static bool IsPrime(int number)
        {
            bool result = true;
            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
