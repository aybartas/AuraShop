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

            var subClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");


            if (subClaim == null)
                return null;

            return Guid.TryParse(subClaim.Value, out var userId) ? userId : null;
        }
    }
}