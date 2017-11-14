using System;

namespace Mastermind
{
  public class MastermindServiceFactory
  {
    private IConfigurationManager configManager;

    public MastermindServiceFactory(IConfigurationManager configManager)
    {
      this.configManager = configManager;
    }

    public MastermindService CreateService()
    {
      var dependencies = new MastermindDependencies()
      {
        AllowedAttempts = GetAppSettingAsInt(nameof(MastermindDependencies.AllowedAttempts)),
        SecretCodeLength = 4
      };
      return new MastermindService(dependencies);
    }

    private string GetAppSetting(string key)
    {
      var appSetting = configManager.GetAppSetting(key);
      if (string.IsNullOrWhiteSpace(appSetting))
        throw new ApplicationException($"{key} must be defined in your app.config file in <appSettings>.");

      return appSetting;
    }

    public int GetAppSettingAsInt(string key)
    {
      var appSetting = GetAppSetting(key);
      int appSettingAsInt = 0;
      if (int.TryParse(appSetting, out appSettingAsInt))
      {
        return appSettingAsInt;
      }
      else
      {
        throw new ApplicationException($"{key} must be an integer.");
      }
    }
  }
}
