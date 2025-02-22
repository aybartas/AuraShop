// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace AuraShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("ResourceCatalog") { Scopes = { "CatalogRead", "CatalogWrite" } },
            new ApiResource("ResourceDiscount") { Scopes = { "DiscountRead", "DiscountWrite" } },
            new ApiResource("ResourceOrder") { Scopes = { "OrderRead", "OrderWrite" } },
            new ApiResource("ResourceCargo") { Scopes = { "CargoRead", "CargoWrite" } },
            new ApiResource("ResourceBasket") { Scopes = { "BasketRead", "BasketWrite" } }
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("CatalogRead"),
            new ApiScope("CatalogWrite"),
            new ApiScope("DiscountRead"),
            new ApiScope("DiscountWrite"),
            new ApiScope("OrderRead"),
            new ApiScope("OrderWrite"),
            new ApiScope("CargoRead"),
            new ApiScope("CargoWrite"),
            new ApiScope("BasketRead"),
            new ApiScope("BasketWrite")
        };

        public static IEnumerable<Client> Clients => new List<Client>{
            new Client
            {
                ClientId = "web-client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:3000/callback" },
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "CatalogRead",
                    "DiscountRead",
                    "OrderRead",
                    "OrderWrite",
                    "CargoRead",
                    "BasketRead",
                    "BasketWrite"
                }
            },
            new Client
            {
                ClientId = "admin-client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:3000/admin/callback" },
                PostLogoutRedirectUris = { "http://localhost:3000/admin" },
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "CatalogRead", "CatalogWrite",
                    "DiscountRead", "DiscountWrite",
                    "OrderRead", "OrderWrite",
                    "CargoRead", "CargoWrite",
                    "BasketRead", "BasketWrite"
                }
            }

        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}