// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace AuraShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog")
            {
                Scopes = new List<string>(){"CatalogWritePermission","CatalogReadPermission"},
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes = new List<string>(){ "DiscountWritePermission", "DiscountReadPermission"},
            },
            new ApiResource("ResourceOrder")
            {
                Scopes = new List<string>(){ "OrderWritePermission", "OrderReadPermission"},
            },

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogWritePermission","Write authority for catalog operations"),
            new ApiScope("CatalogReadPermission","Read authority for catalog operations"),
            new ApiScope("DiscountWritePermission","Write authority for discount operations"),
            new ApiScope("DiscountReadPermission","Write authority for discount operations"),
            new ApiScope("OrderWritePermission","Full authority for order operations"),
            new ApiScope("OrderReadPermission","Read authority for order operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            // Visitor
            new Client()
            {
                ClientId = "AuraShopVisitorId",
                ClientName = "AuraShop Visitor",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>(){new Secret("AuraShopSecret".Sha256())},
                AllowedScopes = new List<string>(){ "CatalogReadPermission" }
            },

            // Manager
            new Client()
            {
                ClientId = "AuraShopManagerId",
                ClientName = "AuraShop Manager",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>(){new Secret("AuraShopSecret".Sha256())},
                AllowedScopes = new List<string>(){ "CatalogReadPermission", "CatalogWritePermission" }
            },

            // Admin
            new Client()
            {
                ClientId = "AuraShopAdminId",
                ClientName = "AuraShop Admin",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>(){new Secret("AuraShopSecret".Sha256())},
                AllowedScopes = new List<string>()
                {
                    "CatalogReadPermission", "CatalogWritePermission",
                    "DiscountWritePermission", "DiscountReadPermission" ,
                    "OrderWritePermission", "OrderReadPermission",
                     IdentityServerConstants.LocalApi.ScopeName,
                     IdentityServerConstants.StandardScopes.Email ,
                     IdentityServerConstants.StandardScopes.OpenId ,
                     IdentityServerConstants.StandardScopes.Profile,

                },
                AccessTokenLifetime = 600,
            },
        };
    }
}