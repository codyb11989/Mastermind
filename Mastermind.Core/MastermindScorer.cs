using Mastermind.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastermind.Core
{
  public class MastermindScorer
  {
    private readonly string _secretCode;

    public MastermindScorer(string secretCode)
    {
      _secretCode = secretCode;
#if DEBUG
      Console.WriteLine($"Secret Code: {_secretCode}");
#endif
    }


    public async Task<string> Score(string userInput)
    {
      return await Task.Run(() =>
      {
        var secretCode = CodePiece.DecipherCode(_secretCode).ToList();
        var userCode = CodePiece.DecipherCode(userInput).ToList();
        return $"{GetPerfectMatchScore(userCode, secretCode)}{GetCloseMatchScore(userCode, secretCode)}";
      });
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
