namespace AuraShop.Basket.Features.Baskets
{
    public class BasketUserContext(Guid userId, bool isAnonymous)
    {
        public Guid UserId { get; } = userId;
        public bool IsAnonymous { get; } = isAnonymous;
    }

    public interface IBasketAuthService
    {
        BasketUserContext GetUser();
    }

    public class BasketAuthService(IHttpContextAccessor httpContextAccessor) : IBasketAuthService
    {
        public BasketUserContext GetUser()
        {
            var context = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HttpContext available");
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var idClaim = user.FindFirst("sub")?.Value;
                if (Guid.TryParse(idClaim, out var userId))
                    return new BasketUserContext(userId, false);
            }

            var cookies = context.Request.Cookies;
            if (cookies.TryGetValue("anonUserId", out var anonIdStr) && Guid.TryParse(anonIdStr, out var anonId))
                return new BasketUserContext(anonId, true);

            // Create new anon ID cookie if missing
            var newAnonId = Guid.NewGuid();
            context.Response.Cookies.Append("anonUserId", newAnonId.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                IsEssential = true
            });

            return new BasketUserContext(newAnonId, true);
        }
    }

}
