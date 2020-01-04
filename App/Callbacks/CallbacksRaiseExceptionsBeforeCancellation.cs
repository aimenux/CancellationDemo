using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Callbacks
{
    /// <summary>
    /// Raise exception in multiple callbacks registered before cancellation
    /// </summary>
    public static class CallbacksRaiseExceptionsBeforeCancellation
    {
        public static async Task RunAsync(bool throwOnFirstException = false)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
 
            var longRunningTask1 = Task.Run(async () =>
            {
                await using (token.Register(() => throw new Exception("Help1!")))
                {
                    await LongRunningWorkFactory.CreateNonCancellableDelay(2000);
                }
            }, token);

            await LongRunningWorkFactory.CreateNonCancellableDelay(10);
 
            var longRunningTask2 = Task.Run(async () =>
            {
                await using (token.Register(() => throw new Exception("Help2!")))
                {
                    await LongRunningWorkFactory.CreateNonCancellableDelay(2000);
                }
            }, token);

            await LongRunningWorkFactory.CreateNonCancellableDelay(50);
 
            try
            {
                source.Cancel(throwOnFirstException);
            }
            catch (AggregateException ex)
            {
                var messages = ex.InnerExceptions.Select(x => x.Message).ToList();
                if (messages.Contains("Help1!"))
                {
                    ConsoleColor.Red.WriteLine($"{nameof(AggregateException)}: Caught1!");
                }
                if (messages.Contains("Help2!"))
                {
                    ConsoleColor.Red.WriteLine($"{nameof(AggregateException)}: Caught2!");
                }
            }
            catch (Exception ex) when (ex.Message == "Help1!")
            {
                ConsoleColor.Red.WriteLine($"{nameof(Exception)}: Caught1!");
            }
            catch (Exception ex) when (ex.Message == "Help2!")
            {
                ConsoleColor.Red.WriteLine($"{nameof(Exception)}: Caught2!");
            }

            ConsoleColor.Gray.WriteLine($"Task1 status = {longRunningTask1.Status}");
            ConsoleColor.Gray.WriteLine($"Task2 status = {longRunningTask2.Status}");
        }
    }
}