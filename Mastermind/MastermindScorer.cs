using Mastermind.Helpers;
using Mastermind.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
  public class MastermindScorer : IMastermindScorer
  {
    private List<Code> secretCodeList;
    private CodeComparer comparer;

    public MastermindScorer(string secretCode)
    {
      secretCodeList = DecipherCode(secretCode);
      comparer = new CodeComparer();
    }

    public string Score(string userInput)
    {
      var score = new List<string>();
      var userCode = DecipherCode(userInput);

      var perfectMatches = secretCodeList.Intersect(userCode, comparer);
      perfectMatches.ToList().ForEach(m => score.Add("+"));

      var remainingSecretCode = secretCodeList.Except(perfectMatches, comparer).ToList();
      var remainingUserCode = userCode.Except(perfectMatches, comparer).ToList();

      remainingSecretCode.ForEach(secretCode =>
      {
        var code = remainingUserCode.FirstOrDefault(c => c.Value == secretCode.Value);
        if (code != null)
        {
          score.Add("-");
          remainingUserCode.Remove(code);
        }
      });

      return string.Concat(score.OrderByDescending(c => c));
    }

    private List<Code> DecipherCode(string code)
    {
      var codeList = new List<Code>();
      for (int i = 0; i < code?.Length; i++)
      {
        codeList.Add(new Code() { Value = code[i], Index = i });
      }
      return codeList;
    }
  }
}
