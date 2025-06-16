using AuraShop.Auth.Models;
using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.Login;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<TokenResponse>> HandleAsync(LoginRequest request)
    {
        var response = await keycloak.LoginAsync(request.Username, request.Password);
        return ServiceResult<TokenResponse>.SuccessAsOk(response);
    }
}