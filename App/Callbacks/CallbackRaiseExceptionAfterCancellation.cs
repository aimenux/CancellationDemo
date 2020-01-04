using System;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace App.Callbacks
{
    public static class CallbackRaiseExceptionAfterCancellation
    {
        public static async Task RunAsync()
        {
            using var source = new CancellationTokenSource();
            var token = source.Token;
            source.Cancel();

            try
            {
                await using (token.Register(() => throw new Exception("Help!")))
                {
                    await LongRunningWorkFactory.CreateNonCancellableDelay();
                }
            }
            catch (Exception ex) when (ex.Message == "Help!")
            {
                ConsoleColor.Red.WriteLine($"{nameof(Exception)}: Caught!");
            }
        }
    }
}
