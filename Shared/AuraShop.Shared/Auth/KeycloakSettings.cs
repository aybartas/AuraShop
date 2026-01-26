namespace AuraShop.Shared.Auth;

public class KeycloakSettings
{
    public const string SectionName = "Keycloak";
    
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Realm { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = false;

    public string RealmUrl => $"{Authority}/realms/{Realm}";
}
