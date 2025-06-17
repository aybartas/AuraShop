using AuraShop.Auth.Dtos;
using AuraShop.Auth.Models;
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
    public UserDto User { get; set; }
}

public class LoginHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var response = await keycloak.LoginAsync(request.Username, request.Password);

        var userInfo = await keycloak.GetUserInfoAsync(response.AccessToken);

        return ServiceResult<LoginResponse>.SuccessAsOk(new LoginResponse
        {
            AccessToken = response.AccessToken,
            User = new UserDto
            {
                Email = userInfo.Email,
                Username = userInfo.PreferredUsername
            }
        });
    }
}