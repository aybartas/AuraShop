using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace AuraShop.Shared.Auth;

public class TokenForwardingDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = httpContextAccessor.HttpContext?
            .Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader["Bearer ".Length..].Trim();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
