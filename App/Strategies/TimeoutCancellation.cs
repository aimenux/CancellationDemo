using System;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Strategies
{
    /// <summary>
    /// Automatic cancellation of a cancellable long running work using a timeout delay
    /// </summary>
    public static class TimeoutCancellation
    {
        public static async Task RunAsync(TimeSpan timeout)
        {
            var cancellationTokenSource = new CancellationTokenSource(timeout);
            var cancellableWorkTask = LongRunningWorkFactory.CreateCancellableLongRunningWork(cancellationTokenSource.Token);

            try
            {
                await cancellableWorkTask;
            }
            catch (OperationCanceledException ex)
            {
                ConsoleColor.Red.WriteLine($"{nameof(OperationCanceledException)}: {ex.Message}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        public static async Task RunAsync(int timeoutInSeconds)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(1000 * timeoutInSeconds);
            var cancellableWorkTask = LongRunningWorkFactory.CreateCancellableLongRunningWork(cancellationTokenSource.Token);

            try
            {
                await cancellableWorkTask;
            }
            catch (OperationCanceledException ex)
            {
                ConsoleColor.Red.WriteLine($"{nameof(OperationCanceledException)}: {ex.Message}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }
    }
}
