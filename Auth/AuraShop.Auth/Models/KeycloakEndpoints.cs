namespace AuraShop.Auth.Models;

public class KeycloakEndpoints(KeycloakConfig config)
{
    private readonly string _baseUrl = config.BaseUrl.TrimEnd('/');
    private readonly string _realm = config.Realm;

    public string TokenEndpoint => $"{_baseUrl}/realms/{_realm}/protocol/openid-connect/token";
    public string AdminTokenEndpoint => $"{_baseUrl}/realms/{_realm}/protocol/openid-connect/token";
    public string UsersEndpoint() => $"{_baseUrl}/admin/realms/{_realm}/users";
    public string RolesEndpoint(string roleName) => $"{_baseUrl}/admin/realms/{_realm}/roles/{roleName}";
    public string AssignRoleToUserEndpoint(string userId) => $"{_baseUrl}/admin/realms/{_realm}/users/{userId}/role-mappings/realm";
    public string UserInfoEndpoint => $"{_baseUrl}/realms/{_realm}/protocol/openid-connect/userinfo";
}