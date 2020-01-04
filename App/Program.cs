using System.Threading.Tasks;
using App.Callbacks;
using App.Strategies;

namespace App
{
    public static class Program
    {
        private static async Task Main()
        {
            await CancellationCallbacksDemo.MainAsync();
            await CancellationStrategiesDemo.MainAsync();
        }
    }
}
