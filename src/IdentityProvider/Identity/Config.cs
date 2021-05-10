// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
                  new ApiResource("basket", "Basket API")
                {
                    Scopes = { "basket.fullaccess" }
                },
               new ApiResource("ordering", "Ordering API")
                {
                    Scopes = { "ordering.fullaccess" }
                }
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.Phone()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("basket.fullaccess"),
                new ApiScope("ordering.fullaccess")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                   ClientId = "front",
                   ClientName = "JavaScript Client",
                   AllowedGrantTypes = GrantTypes.Implicit,
                   RequireClientSecret = false,

                   AllowAccessTokensViaBrowser = true,
                   RedirectUris =           { "http://localhost:4200/auth-callback" },
                   PostLogoutRedirectUris = { "http://localhost:4200" },
                   AllowedCorsOrigins =     { "http://localhost:4200" },
                   AllowedScopes =
                   {
                         "openid", "profile", "address", "email" ,"phone", "basket.fullaccess", "ordering.fullaccess"
                   }

                }
            };
    }
}