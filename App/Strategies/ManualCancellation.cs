using System;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Strategies
{
    /// <summary>
    /// Manual cancellation of a cancellable long running work using a cancel key
    /// </summary>
    public static class ManualCancellation
    {
        public static async Task RunAsync(char cancelKey)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellableWorkTask = LongRunningWorkFactory.CreateCancellableLongRunningWork(cancellationTokenSource.Token);

            var cancelLowerKey = char.ToLower(cancelKey);
            var cancelUpperKey = char.ToUpper(cancelKey);

            ConsoleColor.Yellow.WriteLine($"Press {cancelLowerKey} or {cancelUpperKey} to cancel long running work");

            var pressedKey = Console.ReadKey().KeyChar;
            if (pressedKey == cancelLowerKey || pressedKey == cancelUpperKey)
            {
                cancellationTokenSource.Cancel();
                ConsoleColor.Yellow.WriteLine("\nTask cancellation requested.");
            }

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
