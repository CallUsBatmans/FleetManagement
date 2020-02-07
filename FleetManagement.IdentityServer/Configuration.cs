using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace FleetManagement.IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>()
            {
                new ApiResource("ApiOne")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        "ApiOne",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile }
                }
            };
        }
    }
}
