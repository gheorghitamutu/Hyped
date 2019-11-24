using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace HypedClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true; // optional
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();
            app.AddComponent<App>("app");
        }
    }
}
