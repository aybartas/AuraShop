using System.Security.Claims;

namespace AuraShop.Shared.Auth;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal)
    {
        var subClaim = principal.FindFirst("sub") ?? principal.FindFirst(ClaimTypes.NameIdentifier);
        if (subClaim == null) return null;
        return Guid.TryParse(subClaim.Value, out var userId) ? userId : null;
    }

    public static string? GetUserName(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("preferred_username")?.Value 
            ?? principal.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("email")?.Value 
            ?? principal.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static string? GetFirstName(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("given_name")?.Value 
            ?? principal.FindFirst(ClaimTypes.GivenName)?.Value;
    }

    public static string? GetLastName(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("family_name")?.Value 
            ?? principal.FindFirst(ClaimTypes.Surname)?.Value;
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
    {
        return principal.FindAll(ClaimTypes.Role).Select(c => c.Value);
    }

    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        return principal.IsInRole(Roles.Admin);
    }

    public static bool IsCustomer(this ClaimsPrincipal principal)
    {
        return principal.IsInRole(Roles.Customer);
    }
}
