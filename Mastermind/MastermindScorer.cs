using Mastermind.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
  public class MastermindScorer : IMastermindScorer
  {
    private readonly string secret;

    public MastermindScorer(string secret)
    {
      this.secret = secret;
    }

    public string Score(string userInput)
    {
      var secretCode = CodePiece.DecipherCode(secret).ToList();
      var userCode = CodePiece.DecipherCode(userInput).ToList();
      return $"{GetPerfectMatchScore(userCode, secretCode)}{GetCloseMatchScore(userCode, secretCode)}";
    }

    private string GetPerfectMatchScore(List<CodePiece> userDecipher, List<CodePiece> secretDecipher) =>
      new string('+', GetPerfectMatches(userDecipher, secretDecipher).Count());

    private string GetCloseMatchScore(List<CodePiece> userDecipher, List<CodePiece> secretDecipher) =>
      new string('-', GetCloseMatches(userDecipher, secretDecipher).Count());

    private IEnumerable<CodePiece> GetPerfectMatches(List<CodePiece> userDecipher, List<CodePiece> secretDecipher)
    {
      return userDecipher.Intersect(secretDecipher).Select(x =>
      {
        x.PerfectMatch = true;
        return x;
      });
    }

    private IEnumerable<CodePiece> GetCloseMatches(List<CodePiece> userDecipher, List<CodePiece> secretDecipher)
    {
      foreach (var secretCodePiece in secretDecipher.Except(userDecipher.Where(x => x.PerfectMatch)))
      {
        foreach (var userCodePiece in userDecipher.Where(x => !x.PerfectMatch && !x.CloseMatch))
        {
          if (userCodePiece.Value == secretCodePiece.Value)
          {
            userCodePiece.CloseMatch = true;
            yield return userCodePiece;
            break;
          }
        }
      }
    }
  }
}