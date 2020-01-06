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
        public string token { get; set; }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsIdentity = (token != null) ? new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, token)
            }, "token")
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
