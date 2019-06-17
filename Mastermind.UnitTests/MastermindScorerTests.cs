using Mastermind.Core;
using Mastermind.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Mastermind.UnitTests
{
  [TestClass]
  public class MastermindScorerTests
  {
    [DataTestMethod]
    [DataRow("1234", null, "")]
    [DataRow("1234", "", "")]
    [DataRow("1234", "NaN", "")]
    [DataRow("1234", "4321", "----")]
    [DataRow("1234", "1214", "+++")]
    [DataRow("1234", "4231", "++--")]
    [DataRow("1234", "1", "+")]
    [DataRow("1111", "1112", "+++")]
    [DataRow("1122", "2211", "----")]
    [DataRow("1212", "2112", "++--")]
    [DataRow("4422", "2222", "++")]
    [DataRow("2644", "2444", "+++")]
    [DataRow("4442", "2333", "-")]
    [DataRow("6652", "1126", "--")]
    [DataRow("5413", "4314", "+--")]
    public async Task MastermindCore_MastermindScorer(string secret, string userInput, string expected)
    {
      // Arrange
      var settings = new Mock<IOptionsMonitor<MastermindSettings>>();
      settings.Setup(x => x.CurrentValue).Returns(new MastermindSettings()
      {
        AllowedAttempts = 10,
        SecretCodeLength = 4
      });
      var service = new MastermindService(settings.Object);
      var scorer = new MastermindScorer(secret);
      var random = new Random();

      // Act
      for (int i = 0; i < random.Next(0, 20); i++)
      {
        //ensures running the Score logic doesn't affect multiple scores on same instance
        await scorer.Score(service.GenerateSecretCode());
      }
      var result = await scorer.Score(userInput);

      // Assert
      Assert.AreEqual(expected, result);
    }
  }
}
