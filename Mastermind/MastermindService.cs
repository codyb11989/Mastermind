using System;

namespace Mastermind
{
  public class MastermindService
  {
    private MastermindDependencies dependencies;

    public MastermindService(MastermindDependencies dependencies)
    {
      this.dependencies = dependencies;
    }

    public void StartNewGame()
    {
      dependencies.Scorer = new MastermindScorer(GenerateSecretCode());
    }

    private string GenerateSecretCode()
    {
      var random = new Random();
      var secretCode = "";
      for (int i = 0; i < dependencies.SecretCodeLength; i++)
      {
        secretCode += random.Next(1, 7); //learned the minValue is included but the maxValue is not
      }
      return secretCode;
    }

    public string GetInsructions()
    {
      return $@"Secret code is {dependencies.SecretCodeLength} digits long and each digit can be between 1 and 6
Plus sign (+) represents correct digit correct position
Minus sign (-) represents correct digit wrong position";
    }

    public int AllowedAttempts()
    {
      return dependencies.AllowedAttempts;
    }

    public string CheckUserInput(string userInput)
    {
      return dependencies.Scorer.Score(userInput);
    }

    public bool IsCorrectGuess(string userScore)
    {
      return userScore == new string('+', dependencies.SecretCodeLength);
    }

    public string CorrectGuessMessage()
    {
      return "You solved it!";
    }

    public string OutOfAttemptsMessage()
    {
      return "You lose :(";
    }
  }
}
