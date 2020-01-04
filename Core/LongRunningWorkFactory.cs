using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public static class LongRunningWorkFactory
    {
        public static Task CreateCancellableLongRunningWork(CancellationToken cancellationToken, int maxIterations = 60)
        {
            return Task.Run(async () =>
            {
                for (var index = 1; index <= maxIterations; index++)
                {
                    await CreateNonCancellableDelay();

                    if (cancellationToken.IsCancellationRequested)
                    {
                        ConsoleColor.Gray.WriteLine($"Cancelled on iteration # {index}");
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    ConsoleColor.Gray.WriteLine($"[CancellableLongRunning] Iteration {index}/{maxIterations} completed");
                }
            }, cancellationToken);
        }

        public static Task CreateNonCancellableLongRunningWork(int maxIterations = 60)
        {
            return Task.Run(async () =>
            {
                for (var index = 1; index <= maxIterations; index++)
                {
                    await CreateNonCancellableDelay();

                    ConsoleColor.Gray.WriteLine($"[NonCancellableLongRunning] Iteration {index}/{maxIterations} completed");
                }
            });
        }

        public static Task CreateNonCancellableDelay(int value = 1000)
        {
            var delay = TimeSpan.FromMilliseconds(value);
            return Task.Delay(delay);
        }
    }
}
