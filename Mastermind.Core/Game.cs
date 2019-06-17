using Mastermind.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mastermind.Core
{
  public class Game
  {
    private readonly IMastermindService _mastermindService;

    public Game(IMastermindService mastermindService)
    {
      _mastermindService = mastermindService;
    }

    public async Task Run()
    {
      do
      {
        Console.Clear();
        Console.WriteLine("***MASTERMIND***\n");
        await Play();
        Console.Write("Do you want to play again (Y/N)?");
      }
      while (Console.ReadLine().ToUpper() == "Y");
    }

    public async Task Play()
    {
      var scorer = _mastermindService.StartNewGame();

      Console.WriteLine(_mastermindService.GetInstructions());

      for (int attemptsLeft = _mastermindService.GetAllowedAttempts(); attemptsLeft > 0; attemptsLeft--)
      {
        Console.WriteLine($"\nEnter your guess ({attemptsLeft} guesses remaining)");

        var result = await scorer.Score(Console.ReadLine());
        if (_mastermindService.IsCorrectGuess(result))
        {
          Console.WriteLine(_mastermindService.GetCorrectGuessMessage());
          return;
        }
        else
        {
          Console.WriteLine(result);
        }
      }
      Console.WriteLine($"\n{_mastermindService.GetOutOfAttemptsMessage()}");
    }
  }
}
