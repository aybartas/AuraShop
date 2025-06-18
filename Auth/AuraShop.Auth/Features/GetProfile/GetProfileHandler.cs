using AuraShop.Auth.Dtos;
using AuraShop.Auth.Models;
using AuraShop.Auth.Services;
using AuraShop.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuraShop.Auth.Features.GetProfile;

public class GetProfileHandler
{
    public ServiceResult<UserDto> HandleAsync(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
            return ServiceResult<UserDto>.Unauthorized();

        var identity = user.Identity as ClaimsIdentity;
        if (identity == null)
            return ServiceResult<UserDto>.Unauthorized();

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

        return ServiceResult<UserDto>.SuccessAsOk(userDto);
    }
}