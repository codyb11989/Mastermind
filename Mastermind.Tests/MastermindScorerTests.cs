using NUnit.Framework;
using Shouldly;
using System.Collections;

namespace Mastermind.Tests
{
  [TestFixture]
  public class MastermindScorerTests
  {
    [Test, TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
    public void MastermindScorer(string secretCode, string userGuess, string result)
    {
      IMastermindScorer scorer = new MastermindScorer(secretCode);
      scorer.Score("5615"); //ensures running the Score logic doesn't affect multiple scores on same instance      
      scorer.Score(userGuess).ShouldBe(result);
    }
  }

  class TestData
  {
    public static IEnumerable TestCases
    {
      get
      {
        yield return new TestCaseData("1234", "4321", "----");
        yield return new TestCaseData("1234", "1214", "+++");
        yield return new TestCaseData("1234", "4231", "++--");
        yield return new TestCaseData("1234", "1234", "++++");
        yield return new TestCaseData("1234", "1", "+");
        yield return new TestCaseData("1234", "2345", "---");
        yield return new TestCaseData("1234", null, "");
        yield return new TestCaseData("1234", "", "");
        yield return new TestCaseData("1234", "NaN", "");
        yield return new TestCaseData("1111", "1112", "+++");
        yield return new TestCaseData("1122", "2211", "----");
        yield return new TestCaseData("1212", "2112", "++--");
        yield return new TestCaseData("4422", "2222", "++");
        yield return new TestCaseData("2644", "2444", "+++");
        yield return new TestCaseData("4442", "2333", "-");
        yield return new TestCaseData("6652", "1126", "--");
      }
    }
  }
}
