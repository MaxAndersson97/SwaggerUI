using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore.IdentityServer4.IdentityConfiguration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "inventoryApi",
                    ClientName = "ASP.NET Core Inventory Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                    AllowedScopes = new List<string> {"getToken"}
                },

            };
        }
    }
}
