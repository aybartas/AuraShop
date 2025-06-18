using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.Register;
public record RegisterRequest(string Email, string Password);

public class RegisterHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<string>> HandleAsync(RegisterRequest request)
    {
        var createdUserId = await keycloak.RegisterUserAsync(request.Email, request.Password);

        return ServiceResult<string>.SuccessAsOk(createdUserId);
    }
}