using System;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Strategies
{
    /// <summary>
    /// Composite cancellation of a cancellable long running work using multiple cancellation tokens
    /// </summary>
    public static class CompositeCancellation
    {
        public static async Task RunAsync(CancellationToken cancellationToken1, CancellationToken cancellationToken2)
        {
            var compositeTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken1, cancellationToken2);

            var cancellableWorkTask = LongRunningWorkFactory.CreateCancellableLongRunningWork(compositeTokenSource.Token);

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
                compositeTokenSource.Dispose();
            }
        }
    }
}
