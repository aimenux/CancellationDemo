using System;
using System.Threading;
using System.Threading.Tasks;
using App.Strategies;
using Core;

namespace App
{
    public static class Program
    {
        private static async Task Main()
        {
            ConsoleColor.Blue.WriteLine("Cancellation strategies demo");

            const char cancelKey = 'Q';
            ConsoleColor.Green.WriteLine($"\nStarting {nameof(ManualCancellation)} - CancelKey [{cancelKey}]");
            await ManualCancellation.RunAsync(cancelKey);

            var timeout1 = TimeSpan.FromMilliseconds(GetRandomNumber());
            ConsoleColor.Green.WriteLine($"\nStarting {nameof(TimeoutCancellation)} - Timeout [{timeout1}]");
            await TimeoutCancellation.RunAsync(timeout1);

            var timeout2 = TimeSpan.FromMilliseconds(GetRandomNumber());
            using var cancellationTokenSource = new CancellationTokenSource(timeout2);
            ConsoleColor.Green.WriteLine($"\nStarting {nameof(WrappedCancellation)} - Timeout [{timeout2}]");
            await WrappedCancellation.RunAsync(cancellationTokenSource.Token);

            using var cancellationTokenSource1 = new CancellationTokenSource(timeout1);
            using var cancellationTokenSource2 = new CancellationTokenSource(timeout2);
            ConsoleColor.Green.WriteLine($"\nStarting {nameof(CompositeCancellation)} - Timeouts [{timeout1} {timeout2}]");
            await CompositeCancellation.RunAsync(cancellationTokenSource1.Token, cancellationTokenSource2.Token);

            ConsoleColor.Green.WriteLine("\nPress any key to exit ...\n");
            Console.ReadKey();
        }

        private static int GetRandomNumber(int min = 500, int max = 2000)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }
    }
}
