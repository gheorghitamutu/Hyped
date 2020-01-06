using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using HypedClient.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;

namespace HypedClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredSessionStorage();
            services.AddAuthorizationCore();
            services.AddScoped<DummyAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<DummyAuthStateProvider>());
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
