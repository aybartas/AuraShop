namespace AuraShop.Auth.Models;

public class KeycloakEndpoints(string baseUrl, string realm)
{
    private readonly string _baseUrl = baseUrl.TrimEnd('/');
    public string TokenEndpoint() => $"{_baseUrl}/realms/{realm}/protocol/openid-connect/token";
    public string AdminTokenEndpoint() => $"{_baseUrl}/realms/master/protocol/openid-connect/token";
    public string UsersEndpoint() => $"{_baseUrl}/admin/realms/{realm}/users";
    public string RolesEndpoint(string roleName) => $"{_baseUrl}/admin/realms/{realm}/roles/{roleName}";
    public string AssignRoleToUserEndpoint(string userId) => $"{_baseUrl}/admin/realms/{realm}/users/{userId}/role-mappings/realm";
}