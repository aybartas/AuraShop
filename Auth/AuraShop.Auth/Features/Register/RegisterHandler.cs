using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.Register;
public record RegisterRequest(string Username, string Password, string Email);


public class RegisterHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<string>> HandleAsync(RegisterRequest request)
    {
       var createdUserId =  await keycloak.RegisterUserAsync(request.Username, request.Password, request.Email);

       return ServiceResult<string>.SuccessAsOk(createdUserId);
    }
}