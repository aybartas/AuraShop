using System.Text.Json.Serialization;

namespace AuraShop.Auth.Models;

public class UserRegistrationRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    [JsonPropertyName("credentials")]
    public Credential[] Credentials { get; set; } = null!;
}