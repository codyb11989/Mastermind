using System;
using System.Configuration;

namespace Mastermind.App
{
  class Program
  {
    static void Main(string[] args)
    {
      do
      {
        Console.WriteLine("***MASTERMIND***\n");
        Play();
        Console.Write("Do you want to play again (Y/N)?");
      }
      while (Console.ReadLine().ToUpper() == "Y");
    }

    private static void Play()
    {
      var service = new MastermindServiceFactory(new ConfigManager()).CreateService();
      service.StartNewGame();

      Console.WriteLine(service.GetInsructions());      

      for (int attemptsLeft = service.AllowedAttempts(); attemptsLeft > 0; attemptsLeft--)
      {
        Console.WriteLine($"\nEnter your guess ({attemptsLeft} guesses remaining)");

        var result = service.CheckUserInput(Console.ReadLine());
        if (service.IsCorrectGuess(result))
        {
          Console.WriteLine(service.CorrectGuessMessage());
          return;
        }
        else
        {
          Console.WriteLine(result);
        }
      }
      Console.WriteLine($"\n{service.OutOfAttemptsMessage()}");
    }
  }

  class ConfigManager : IConfigurationManager
  {
    public string GetAppSetting(string key)
    {
      return ConfigurationManager.AppSettings[key];
    }
  }
}
