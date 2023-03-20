using System.ComponentModel;
using System.Text.Json.Serialization;
using MessagePack;
using TerraformPluginDotNet.Resources;
using TerraformPluginDotNet.Serialization;

namespace PurviewProvider;

[SchemaVersion(1)]
[MessagePackObject]
public record PurviewAccountResource
{

    [Key("id")]
    [Computed]
    [Description("Unique ID for this resource.")]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Id { get; set; }

    [Key("name")]
    [JsonPropertyName("name")]
    [Description("Purview account name")]
    [Required]
    public string Name { get; set; }

    [Key("location")]
    [JsonPropertyName("location")]
    [Description("Azure location")]
    [Required]
    public string Location { get; set; }

    [Key("resource_group_name")]
    [JsonPropertyName("resource_group_name")]
    [Description("Azure resource group name")]
    [Required]
    public string ResourceGroupName { get; set; }


};