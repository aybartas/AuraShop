namespace AuraShop.Auth
{
    public class KeycloakConfig
    {
        public string BaseUrl { get; set; } = null!;
        public string Realm { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string AdminUser { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}
