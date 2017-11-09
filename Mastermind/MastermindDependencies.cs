namespace Mastermind
{
  public class MastermindDependencies
  {
    public int AllowedAttempts { get; set; }
    public int SecretCodeLength { get; set; }
    public IMastermindScorer Scorer { get; set; }
  }
}
