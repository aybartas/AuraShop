using AuraShop.Auth.Services;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Auth.Features.Login;

public class LoginHandler(KeycloakService keycloak) : IRequestHandler<LoginRequest, ServiceResult<LoginResponse>>
{
    public async Task<ServiceResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await keycloak.LoginAsync(request.Email, request.Password);

        return ServiceResult<LoginResponse>.SuccessAsOk(new LoginResponse
        {
            AccessToken = response.AccessToken,
        });
    }
}