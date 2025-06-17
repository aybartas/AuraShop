using AuraShop.Auth.Dtos;
using AuraShop.Auth.Services;
using AuraShop.Shared;

namespace AuraShop.Auth.Features.GetProfile;

public class GetProfileHandler(KeycloakService keycloak)
{
    public async Task<ServiceResult<UserDto>> HandleAsync(GetProfileRequest request)
    {
        var userInfo = await keycloak.GetUserInfoAsync(request.AccessToken);

        return ServiceResult<UserDto>.SuccessAsOk(new UserDto
        {
            Email = userInfo.Email,
            Username = userInfo.PreferredUsername
        });
    }
}