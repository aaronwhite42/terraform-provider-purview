using Microsoft.Extensions.DependencyInjection;
using PurviewProvider;
using TerraformPluginDotNet;
using TerraformPluginDotNet.ResourceProvider;

await TerraformPluginHost.RunAsync(args, "advocacy.dev/aaron/purview", (services, registry) =>
{
    services.AddSingleton<ProviderConfigurator>();
    services.AddTerraformProviderConfigurator<Configuration, ProviderConfigurator>();
    services.AddArmClient();
    services.AddSingleton<IResourceProvider<PurviewAccountResource>, PurviewAccountResourceProvider>();
    registry.RegisterResource<PurviewAccountResource>("purview_account");
});
