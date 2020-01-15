using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace HypedClient.Auth
{
    public class DummyAuthStateProvider : AuthenticationStateProvider
    {
        public string token { get; set; }
        private bool IsLogedIn = false;
        private readonly ILocalStorageService localStorage;
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

        public async Task KeepSession()
        {
            while (IsLogedIn)
            {
                string storagedToken = await localStorage.GetItemAsync<string>("token");
                var claimsIdentity = (storagedToken != null) ? new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, storagedToken)
                }, "token")
                :
                new ClaimsIdentity();
                if (claimsIdentity.Claims.Count() == 0)
                {
                    IsLogedIn = false;
                    //new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
                }
                else
                {
                    new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
                    await Task.Delay(1000);  
                }
                
            }
        }

        public async Task NotifyStateChange()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
