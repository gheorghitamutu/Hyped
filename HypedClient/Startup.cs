using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using HypedClient.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Blazored.Modal;

namespace HypedClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddBlazoredSessionStorage();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddBlazoredModal();
            services.AddScoped<DummyAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<DummyAuthStateProvider>());
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
