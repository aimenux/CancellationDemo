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
            var taskCompletionSource = new TaskCompletionSource<object>();

            var nonCancellableWorkTask = LongRunningWorkFactory.CreateNonCancellableLongRunningWork();

            cancellationToken.Register(() =>
            {
                taskCompletionSource.TrySetCanceled();
            });

            var completedTask = await Task.WhenAny(nonCancellableWorkTask, taskCompletionSource.Task);

            var cancelledOrCompleted = completedTask == taskCompletionSource.Task
                ? "cancelled"
                : "completed";

            ConsoleColor.Yellow.WriteLine($"\nTask is {cancelledOrCompleted}");
        }
    }
}
