using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Purview;
using Microsoft.Extensions.DependencyInjection;

namespace PurviewProvider;

public static class ServiceExtensions
{
    public static IServiceCollection AddArmClient(this IServiceCollection services)
    {
        var credentials = new DefaultAzureCredential();
        var client = new ArmClient(credentials);
        services.AddScoped(c => client);
        // var purviewManagementClient = new PurviewManagementClient(context.AzureAuthentication.SubscriptionId, credentials);
        // purviewManagementClient.BaseUri = new System.Uri("https://management.azure.com/");
        return services;
    }
}