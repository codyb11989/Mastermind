using Mastermind.Core.Configuration;
using Mastermind.Core.Interfaces;
using Microsoft.Extensions.Options;
using System;

namespace Mastermind.Core
{
  public class MastermindService : IMastermindService
  {
    private readonly MastermindSettings _settings;

    public MastermindService(IOptionsMonitor<MastermindSettings> settings)
    {
      _settings = settings.CurrentValue;
    }

    public string GenerateSecretCode()
    {
      var secretCode = "";
      var random = new Random();
      for (int i = 0; i < _settings.SecretCodeLength; i++)
      {
        secretCode += random.Next(1, 7); //learned the minValue is included but the maxValue is not
      }
      return secretCode;
    }

    public int GetAllowedAttempts()
    {
      return _settings.AllowedAttempts;
    }

    public string GetCorrectGuessMessage()
    {
      return _settings.CorrectGuessMessage;
    }

    public string GetInstructions()
    {
      return $@"Secret code is {_settings.SecretCodeLength} digits long and each digit can be between 1 and 6
Plus sign (+) represents correct digit correct position
Minus sign (-) represents correct digit wrong position";
    }

    public string GetOutOfAttemptsMessage()
    {
      return _settings.OutOfAttemptsMessage;
    }

    public bool IsCorrectGuess(string userScore)
    {
      return userScore == new string('+', _settings.SecretCodeLength);
    }

    public MastermindScorer StartNewGame()
    {
      return new MastermindScorer(GenerateSecretCode());
    }
  }
}
