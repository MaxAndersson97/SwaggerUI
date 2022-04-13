using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore.IdentityServer4.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("getToken", "Read Access to Inventory Api"),
                new ApiScope("inventoryApi.write", "Write Access to Inventory Api"),
            };
        }
    }
}
