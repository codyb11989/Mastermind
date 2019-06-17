using Mastermind.Core;
using Mastermind.Core.Configuration;
using Mastermind.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastermind.ConsoleApp
{
  public class Startup
  {
    private readonly IServiceCollection _serviceCollection;
    private readonly IConfiguration _configuration;

    public Startup()
    {
      _serviceCollection = new ServiceCollection();
      var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      _configuration = builder.Build();
    }

    public ServiceProvider BuildServiceProvider()
    {
      ConfigureServices();
      return _serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices()
    {
      _serviceCollection
          .Configure<MastermindSettings>(_configuration.GetSection(nameof(MastermindSettings)))
          .AddTransient<IMastermindService, MastermindService>()
          .AddTransient<Game>();
    }
  }
}
