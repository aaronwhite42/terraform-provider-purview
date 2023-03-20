
using Azure;
using Azure.Analytics.Purview.Account;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Models;
using Azure.ResourceManager.Purview;
using Azure.ResourceManager.Purview.Models;
using TerraformPluginDotNet.ResourceProvider;

namespace PurviewProvider;

public class PurviewAccountResourceProvider : IResourceProvider<PurviewAccountResource>
{
    private readonly ArmClient client;
    private readonly Configuration? configuration;

    public PurviewAccountResourceProvider(ProviderConfigurator purviewConfigurator, ArmClient client)
    {
        this.client = client;
        configuration = purviewConfigurator.Configuration;
    }

    public async Task<PurviewAccountResource> PlanAsync(PurviewAccountResource? prior, PurviewAccountResource proposed)
    {
        // validate Purview account name is available
        var subscription = client.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{configuration?.SubscriptionId}"));

        var accountNameAvailability = await subscription.CheckPurviewAccountNameAvailabilityAsync(
            new PurviewAccountNameAvailabilityContent
            {
                Name = proposed.Name,
                ResourceType = "Microsoft.Purview/accounts"
            }
        );

        if (proposed.Id is null &&
            (accountNameAvailability.Value.IsNameAvailable == null
            || (bool)!accountNameAvailability.Value.IsNameAvailable))
        {
            throw new TerraformResourceProviderException(accountNameAvailability.Value.Message);
        }

        return proposed;
    }

    public async Task<PurviewAccountResource> CreateAsync(PurviewAccountResource planned)
    {
        try
        {
            var resourceGroup = client.GetResourceGroupResource(new ResourceIdentifier($"/subscriptions/{configuration?.SubscriptionId}/resourceGroups/{planned.ResourceGroupName}"));
            var purviewAccountCollection = resourceGroup.GetPurviewAccounts();
            var location = new AzureLocation(planned.Location);

            // account-data https://learn.microsoft.com/en-us/dotnet/api/azure.resourcemanager.purview.purviewaccountdata?view=azure-dotnet
            var purviewAccountData = new PurviewAccountData(location)
            {
                Identity = new ManagedServiceIdentity(ManagedServiceIdentityType.SystemAssigned),
                Location = location
            };

            // generate a new Id if the current value is null
            planned.Id ??= Guid.NewGuid().ToString();

            var result = await purviewAccountCollection.CreateOrUpdateAsync(
                WaitUntil.Completed,
                planned.Name,
                purviewAccountData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return planned;
    }

    public async Task<PurviewAccountResource> ReadAsync(PurviewAccountResource resource)
    {
        //https://learn.microsoft.com/en-us/dotnet/api/overview/azure/analytics.purview.account-readme?view=azure-dotnet-preview
        // var purviewAccountClient = new PurviewAccountClient(new Uri($"https://{resource.Name}.purview.azure.com"), new DefaultAzureCredential());
        // var response = await purviewAccountClient.GetAccountPropertiesAsync();
        return resource;
    }

    public Task<PurviewAccountResource> UpdateAsync(PurviewAccountResource? prior, PurviewAccountResource planned)
    {
        //TODO
        return Task.FromResult(planned);
    }

    public Task DeleteAsync(PurviewAccountResource resource)
    {
        //TODO
        return Task.FromResult(resource);
    }

    public Task<IList<PurviewAccountResource>> ImportAsync(string id)
    {
        throw new NotImplementedException();
    }
}