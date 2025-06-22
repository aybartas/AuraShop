using AuraShop.Auth.Services;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Auth.Features.Register;
public record RegisterRequest(string Email, string Password): IRequest<ServiceResult<string>>;

public class RegisterHandler(KeycloakService keycloak) : IRequestHandler<RegisterRequest, ServiceResult<string>>
{
    public async Task<ServiceResult<string>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var createdUserId = await keycloak.RegisterUserAsync(request.Email, request.Password);

        return ServiceResult<string>.SuccessAsOk(createdUserId);
    }
}