using AuraShop.Auth.Features.GetProfile;
using AuraShop.Auth.Features.Login;
using AuraShop.Auth.Features.Register;

namespace AuraShop.Auth.Features
{
    public static class AuthEndpointExtensions
    {
        public static void AddAuthEndpoints(this WebApplication app)
        {
            app.MapGroup("api/auth").WithTags("Auth").AddLoginEndpoint().AddRegisterEndpoint().AddGetProfileEndpoint();
        }
    }
}