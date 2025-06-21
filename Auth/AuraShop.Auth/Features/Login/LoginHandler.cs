using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.Login;

public class LoginHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var response = await keycloak.LoginAsync(request.Email, request.Password);

        return ServiceResult<LoginResponse>.SuccessAsOk(new LoginResponse
        {
            AccessToken = response.AccessToken,
        });
    }
}