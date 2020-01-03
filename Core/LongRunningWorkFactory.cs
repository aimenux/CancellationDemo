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
                    await WaitNonCancellableDelay();

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
                    await WaitNonCancellableDelay();

                    ConsoleColor.Gray.WriteLine($"[NonCancellableLongRunning] Iteration {index}/{maxIterations} completed");
                }
            });
        }

        private static Task WaitNonCancellableDelay()
        {
            var delay = TimeSpan.FromMilliseconds(1000);
            return Task.Delay(delay);
        }
    }
}
