using MessagePack;
using System.ComponentModel;
using TerraformPluginDotNet.Resources;

namespace PurviewProvider;

[MessagePackObject]
public class Configuration
{
    [Key("subscription_id")]
    [Description("Subscription Id")]
    [Required]
    public string? SubscriptionId { get; set; }
}