using Duende.IdentityServer.Models;

namespace AuraShop.AuthorizationServer
{
    public static class Config
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<IdentityResource> IdentityResources => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static List<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("read", "Read"),
            new ApiScope("write", "Write"),
        };

        public static List<Client> Clients => new List<Client>
        {
           new Client
           {
              ClientId = "client",
              ClientSecrets = { new Secret("secret".Sha256())},
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              AllowedScopes = {"api1","api2.readonly"}
           },
           new Client
           {
               ClientId = "client",
               ClientSecrets = { new Secret("secret".Sha256())},
               AllowedGrantTypes = GrantTypes.Code,
               AllowedScopes = { "read", "write" }
           }
        };

        // 29.50

    }
}
