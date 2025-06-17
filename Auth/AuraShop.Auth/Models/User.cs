using System.Text.Json.Serialization;

namespace AuraShop.Auth.Models
{
    public class User
    {
        [JsonPropertyName("sub")]
        public string Sub { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonPropertyName("preferred_username")]
        public string PreferredUsername { get; set; } = null!;
    }
}
