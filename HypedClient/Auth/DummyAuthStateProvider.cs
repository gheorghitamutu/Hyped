using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HypedClient.Auth
{
    public class DummyAuthStateProvider : AuthenticationStateProvider
    {
        public static bool Authenticated { get; set; }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsIdentity = Authenticated ? new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test_user")
            }, "server_auth")
            :
            new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        }

        public async Task NotifyStateChange()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
