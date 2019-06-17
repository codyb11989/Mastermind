using Microsoft.Extensions.DependencyInjection;
using Mastermind.Core;
using System.Threading.Tasks;

namespace Mastermind.ConsoleApp
{
  class Program
  {
    public static void Main()
    {
      MainAsync().GetAwaiter().GetResult();
    }

    private static async Task MainAsync()
    {
      using (var serviceProvider = new Startup().BuildServiceProvider())
      {
        var game = serviceProvider.GetService<Game>();
        await game.Run();
      }
    }
  }
}
