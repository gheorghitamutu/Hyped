using Microsoft.AspNetCore.Blazor.Hosting;
using Serilog;
using System;
using Serilog.Core;

namespace HypedClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var levelSwitch = new LoggingLevelSwitch();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
                // .WriteTo.BrowserHttp() // -> https://nblumhardt.com/2019/11/serilog-blazor | https://docs.microsoft.com/en-us/aspnet/core/blazor/debug?view=aspnetcore-3.1
                .WriteTo.BrowserConsole(levelSwitch: levelSwitch)
                .CreateLogger();

            Log.Information("Hello, browser!");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An exception occurred while creating the WASM host");
                throw;
            }
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
