using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Callbacks
{
    /// <summary>
    /// Raise exception in a callback registered before cancellation
    /// </summary>
    public static class CallbackRaiseExceptionBeforeCancellation
    {
        public static async Task RunAsync()
        {
            using var source = new CancellationTokenSource();
            var token = source.Token;

            var longRunningTask = Task.Run(async () =>
            {
                await using (token.Register(() => throw new Exception("Help!")))
                {
                    await LongRunningWorkFactory.CreateNonCancellableDelay(2000);
                }
            }, token);

            await LongRunningWorkFactory.CreateNonCancellableDelay(50);

            try
            {
                source.Cancel();
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Single().Message == "Help!")
            {
                ConsoleColor.Red.WriteLine($"{nameof(AggregateException)}: Caught!");
            }

            ConsoleColor.Gray.WriteLine($"Task status = {longRunningTask.Status}");
        }
    }
}
