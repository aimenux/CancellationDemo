using System;
using System.Threading.Tasks;
using Core;

namespace App.Callbacks
{
    public static class CancellationCallbacksDemo
    {
        public static async Task MainAsync()
        {
            ConsoleColor.Blue.WriteLine("Cancellation callbacks demo");

            ConsoleColor.Green.WriteLine($"\nStarting {nameof(CallbackRaiseExceptionAfterCancellation)}");
            await CallbackRaiseExceptionAfterCancellation.RunAsync();

            ConsoleColor.Green.WriteLine($"\nStarting {nameof(CallbackRaiseExceptionBeforeCancellation)}");
            await CallbackRaiseExceptionBeforeCancellation.RunAsync();

            ConsoleColor.Green.WriteLine($"\nStarting {nameof(CallbacksRaiseExceptionsBeforeCancellation)}");
            await CallbacksRaiseExceptionsBeforeCancellation.RunAsync(true);

            ConsoleColor.Green.WriteLine("\nPress any key to exit ...\n");
            Console.ReadKey();
        }
    }
}
