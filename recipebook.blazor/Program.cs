using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using recipebook.blazor.Configuration;

namespace recipebook.blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Logging.SetMinimumLevel(LogLevel.Warning);

            DependencyInjectionConfig.Register(builder.Services, builder.Configuration, builder.HostEnvironment.BaseAddress);

            builder.Services.AddMsalAuthentication(options =>
            {
                var permission = builder.Configuration["ApiPermission"];

                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(permission);
            });

            await builder.Build().RunAsync();
        }
    }
}
