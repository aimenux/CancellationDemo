using System;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Strategies
{
    /// <summary>
    /// Wrapped cancellation of a non cancellable long running work using a cancellation token
    /// </summary>
    public static class WrappedCancellation
    {
        public static async Task RunAsync(CancellationToken cancellationToken)
        {
            var nonCancellableWorkTask = LongRunningWorkFactory.CreateNonCancellableLongRunningWork();

            try
            {
                await WrapWithCancellation(nonCancellableWorkTask, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                ConsoleColor.Red.WriteLine($"{nameof(OperationCanceledException)}: {ex.Message}");
            }
        }

        public static async Task RunAsync(TimeSpan timeout)
        {
            var nonCancellableWorkTask = LongRunningWorkFactory.CreateNonCancellableLongRunningWork();

            try
            {
                await WrapWithTimeSpan(nonCancellableWorkTask, timeout);
            }
            catch (OperationCanceledException ex)
            {
                ConsoleColor.Red.WriteLine($"{nameof(OperationCanceledException)}: {ex.Message}");
            }
        }

        private static async Task WrapWithCancellation(Task task, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            await using (cancellationToken.Register(() => taskCompletionSource.TrySetCanceled()))
            {
                var completedTask = await Task.WhenAny(task, taskCompletionSource.Task);
                if (completedTask != task)
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }
        }

        private static async Task WrapWithTimeSpan(Task task, TimeSpan timeout)
        {
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout));
            if (completedTask != task)
            {
                throw new OperationCanceledException("Timeout expired", new TimeoutException($"{timeout}"));
            }
        }
    }
}
