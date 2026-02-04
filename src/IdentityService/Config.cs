using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            //
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            // resource that is used to identify the api
            new ApiScope("freightApp", "Freight App full access"),
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                // IdentityResources.OpenId, IdentityResources.Profile, ApiScopes.FreightApp
                AllowedScopes = { "openid", "profile", "freightApp" },
                RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = [new Secret("NotASecretAtAll".Sha256())],
            },
        ];
}
