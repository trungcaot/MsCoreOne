using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace MsCoreOne.Infrastructure.Identity.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
           };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new ApiResource[]
            {
                new ApiResource("api.mscoreone", "MsCoreOne API")
            };

        public static List<TestUser> GetUsers() =>
           new List<TestUser>
           {
                new TestUser
                {
                    SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                    Username = "test",
                    Password = "P@ssw0rd",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Trung"),
                        new Claim("family_name", "Cao")
                    }
                }
           };

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "postman",
                    ClientSecrets = new [] { new Secret("postmansecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "api.mscoreone" }
                },
                new Client
                {
                    ClientId = "mscoreone-swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = {$"{clientUrls["Swagger"]}/swagger/oauth2-redirect.html"},
                    PostLogoutRedirectUris = { $"{clientUrls["Swagger"]}/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = { $"{clientUrls["Swagger"]}" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.mscoreone"
                    }
                },
                new Client
                {
                    ClientId = "mscoreone-mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = { $"{clientUrls["Mvc"]}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{clientUrls["Mvc"]}/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api.mscoreone"
                    },

                    AllowOfflineAccess = true
                 },
                new Client
                {
                    ClientId = "mscoreone-react",
                    ClientName = "mscoreone react",
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenLifetime = 330, // 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 330,
                    AllowAccessTokensViaBrowser = true,

                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = new List<string>
                    {
                        $"{clientUrls["React"]}/authentication/login-callback",
                        $"{clientUrls["React"]}/silent-renew.html",
                        $"{clientUrls["React"]}"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{clientUrls["React"]}/unauthorized",
                        $"{clientUrls["React"]}/authentication/logout-callback",
                        $"{clientUrls["React"]}"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        $"{clientUrls["React"]}"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.mscoreone"
                    }
                }
            };
    }
}
