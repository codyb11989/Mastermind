namespace Mastermind.Core.Configuration
{
  public class MastermindSettings
  {
    public int AllowedAttempts { get; set; }
    public string CorrectGuessMessage { get; set; }
    public string OutOfAttemptsMessage { get; set; }
    public int SecretCodeLength { get; set; }
  }
}
