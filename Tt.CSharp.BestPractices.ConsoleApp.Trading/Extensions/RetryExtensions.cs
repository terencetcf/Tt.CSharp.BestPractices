using System;
using System.Threading.Tasks;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Extensions
{
    public static class RetryExtensions
    {
        public static async Task<T> WithRetry<T>(this Func<Task<T>> action)
        {
            var result = default(T);
            int retryCount = 0;

            bool successful = false;
            do
            {
                try
                {
                    result = await action();
                    successful = true;
                }
                catch (Exception)
                {
                    retryCount++;
                    Console.WriteLine("Failed no. {0}!", retryCount);
                }
            } while (retryCount < 3 && !successful);

            return result;
        }

        public static T WithRetry<T>(this Func<T> action)
        {
            var result = default(T);
            int retryCount = 0;

            bool successful = false;
            do
            {
                try
                {
                    result = action();
                    successful = true;
                }
                catch (Exception)
                {
                    retryCount++;
                    Console.WriteLine("Failed no. {0}!", retryCount);
                }
            } while (retryCount < 3 && !successful);

            return result;
        }

        public static Func<TResult> Partial<TParam1, TResult>(
            this Func<TParam1, TResult> func, TParam1 parameter)
        {
            return () => func(parameter);
        }

        public static Func<TParam1, Func<TResult>> Curry<TParam1, TResult>
            (this Func<TParam1, TResult> func)
        {
            return parameter => () => func(parameter);
        }
    }
}