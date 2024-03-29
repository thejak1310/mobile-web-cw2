﻿using Duende.IdentityServer.Models;

namespace gevs_identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("gevsApp", "GEVS app full access"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientId = "postman",
                    ClientName = "Postman",
                    AllowedScopes = {"openid", "profile", "gevsApp"},
                    RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
                },
                new()
                {
                    ClientId = "nextApp",
                    ClientName = "nextApp",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = false,
                    AllowedScopes = {"openid", "profile", "gevsApp"},
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600*24*30,
                    RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},
                    ClientSecrets = new [] {new Secret("secret".Sha256())},
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
    }
}
