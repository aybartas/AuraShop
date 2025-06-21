using System.Text.Json.Serialization;

namespace AuraShop.Auth.Models;

public class Credential
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = null!;

    [JsonPropertyName("temporary")]
    public bool Temporary { get; set; }
}