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
      new string('+', GetPerfectMatchCount(userDecipher, secretDecipher));

    private string GetCloseMatchScore(List<CodePiece> userDecipher, List<CodePiece> secretDecipher) =>
      new string('-', GetCloseMatchCount(userDecipher, secretDecipher));

    private int GetPerfectMatchCount(List<CodePiece> userDecipher, List<CodePiece> secretDecipher)
    {
      return userDecipher.Intersect(secretDecipher).ToList().Select(x =>
      {
        secretDecipher.Remove(x);
        userDecipher.Remove(x);
        return x;
      }).Count();
    }

    private int GetCloseMatchCount(List<CodePiece> userDecipher, List<CodePiece> secretDecipher)
    {
      return secretDecipher.Count(y =>
      {
        var closeMatch = userDecipher.FirstOrDefault(x => new CloseMatchComparer().Equals(x, y));
        if (closeMatch != null)
        {
          userDecipher.Remove(closeMatch);
          return true;
        }
        return false;
      });
    }
  }
}