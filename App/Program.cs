using System;
using System.Threading.Tasks;
using App.Callbacks;
using App.Strategies;
using Core;

namespace App
{
    public static class Program
    {
        private static async Task Main()
        {
            ConsoleColor.Blue.WriteLine("Please make a choice ?");
            ConsoleColor.Blue.WriteLine($"-> 1 for {nameof(CancellationStrategiesDemo)}");
            ConsoleColor.Blue.WriteLine($"-> 2 for {nameof(CancellationCallbacksDemo)}");

            var inputChoice = Console.ReadLine();
            if (!int.TryParse(inputChoice, out var choice))
            {
                choice = -1;
            }

            switch (choice)
            {
                case 1:
                    await CancellationStrategiesDemo.MainAsync();
                    break;

                case 2:
                    await CancellationStrategiesDemo.MainAsync();
                    break;

                default:
                    ConsoleColor.Red.WriteLine($"Invalid choice {inputChoice} !");
                    ConsoleColor.Green.WriteLine("Press any key to exit ...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
