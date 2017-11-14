
using Mastermind.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
  public class MastermindScorer : IMastermindScorer
  {
    private List<CodePiece> secretCodeList;

    public MastermindScorer(string secretCode)
    {
      secretCodeList = DecipherCode(secretCode);
    }

    public string Score(string userInput)
    {
      var score = new List<string>();
      var userCode = DecipherCode(userInput);

      var perfectMatches = secretCodeList.Intersect(userCode);

      perfectMatches.ToList().ForEach(m => score.Add("+"));

      var imperfectMatches = secretCodeList.Except(perfectMatches);
      foreach (var secretCodePiece in imperfectMatches)
      {
        var userCodePieces = userCode.Except(perfectMatches);
        foreach (var userCodePiece in userCodePieces)
        {
          if ( userCodePiece.Value == secretCodePiece.Value && !userCodePiece.Matched)
          {
            score.Add("-");
            userCodePiece.Matched = true;
          }
        }
      }      

      return string.Concat(score);
    }

    private List<CodePiece> DecipherCode(string code)
    {
      var codeList = new List<CodePiece>();
      for (int i = 0; i < code?.Length; i++)
      {
        codeList.Add(new CodePiece() { Value = code[i], Index = i });
      }
      return codeList;
    }
  }
}
