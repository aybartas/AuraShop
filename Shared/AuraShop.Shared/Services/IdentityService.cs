using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AuraShop.Shared.Services;

public class IdentityService(IHttpContextAccessor contextAccessor) : IIdentityService
{
    public Guid? UserId
    {
        get
        {
            var user = contextAccessor.HttpContext?.User;
            if (user == null)
                return null;

            // Keycloak uses "sub" claim for user id
            var subClaim = user.FindFirst("sub") ?? user.FindFirst(ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return null;

            return Guid.TryParse(subClaim.Value, out var userId) ? userId : null;
        }
    }

    public string? UserName => GetClaimValue("preferred_username") ?? GetClaimValue(ClaimTypes.Name);

    public string? Email => GetClaimValue("email") ?? GetClaimValue(ClaimTypes.Email);

    public IEnumerable<string> Roles
    {
        get
        {
            var user = contextAccessor.HttpContext?.User;
            if (user == null)
                return Enumerable.Empty<string>();

            return user.FindAll(ClaimTypes.Role).Select(c => c.Value);
        }
    }

    public bool IsInRole(string role)
    {
        var user = contextAccessor.HttpContext?.User;
        return user?.IsInRole(role) ?? false;
    }

    public bool IsAuthenticated
    {
        get
        {
            var user = contextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated ?? false;
        }
    }

    private string? GetClaimValue(string claimType)
    {
        var user = contextAccessor.HttpContext?.User;
        return user?.FindFirst(claimType)?.Value;
    }
}