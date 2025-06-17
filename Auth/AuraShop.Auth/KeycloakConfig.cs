namespace AuraShop.Auth
{
    public class KeycloakConfig
    {
        public string BaseUrl { get; set; } = null!;
        public string Realm { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string AdminClientId { get; set; } = null!;
        public string AdminClientSecret { get; set; } = null!;
    }
}
