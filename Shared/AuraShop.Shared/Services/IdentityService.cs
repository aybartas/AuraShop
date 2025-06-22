using Microsoft.AspNetCore.Http;

namespace AuraShop.Shared.Services;

public class IdentityService(IHttpContextAccessor contextAccessor) : IIdentityService
{
    public Guid? UserId
    {
        get
        {
            var user = contextAccessor.HttpContext?.User;
            var id = user?.FindFirst("sub")?.Value;

            return Guid.TryParse(id, out var guid) ? guid : null;
        }
    }
}