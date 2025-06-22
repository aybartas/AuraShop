using AuraShop.Auth.Dtos;

using AuraShop.Shared;
using System.Security.Claims;
using MediatR;

namespace AuraShop.Auth.Features.GetProfile;

public class GetProfileHandler(IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetProfileRequest, ServiceResult<UserDto>>
{

    public Task<ServiceResult<UserDto>> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
            return Task.FromResult(ServiceResult<UserDto>.Unauthorized());

        if (user.Identity is not ClaimsIdentity identity)
            return Task.FromResult(ServiceResult<UserDto>.Unauthorized());

        var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = identity.FindFirst("preferred_username")?.Value;
        var email = identity.FindFirst(ClaimTypes.Email)?.Value;

        var roles = identity.FindAll("role").Select(r => r.Value).ToList();

        var userDto = new UserDto
        {
            Username = username,
            Email = email,
            Roles = roles,
        };

        return Task.FromResult(ServiceResult<UserDto>.SuccessAsOk(userDto));
    }
}