using TerraformPluginDotNet.ProviderConfig;

namespace PurviewProvider;

public class ProviderConfigurator : IProviderConfigurator<Configuration>
{
    public Configuration? Configuration { get; private set; }

    public Task ConfigureAsync(Configuration config)
    {
        Configuration = config;
        return Task.CompletedTask;
    }
}