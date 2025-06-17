using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.Login;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string AccessToken { get; set; }
}

public class LoginHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var response = await keycloak.LoginAsync(request.Username, request.Password);

        return ServiceResult<LoginResponse>.SuccessAsOk(new LoginResponse
        {
            AccessToken = response.AccessToken,
        });
    }
}