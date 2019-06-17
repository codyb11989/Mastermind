namespace Mastermind.Core.Interfaces
{
  public interface IMastermindService
  {
    string GenerateSecretCode();
    string GetInstructions();
    int GetAllowedAttempts();
    string GetCorrectGuessMessage();
    string GetOutOfAttemptsMessage();
    MastermindScorer StartNewGame();
    bool IsCorrectGuess(string userScore);
  }
}
