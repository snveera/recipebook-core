using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace recipebook.blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Logging.SetMinimumLevel(LogLevel.Warning);

            RegisterDefaultHttpClient(builder);
            RegisterGraphHttpClient(builder);

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
            });

            await builder.Build().RunAsync();
        }

        private static void RegisterDefaultHttpClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        }

        private static void RegisterGraphHttpClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient("GraphApi", client =>
                client.BaseAddress = new Uri("https://graph.microsoft.com"))
                    .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                    .ConfigureHandler(new[] { "https://graph.microsoft.com" }, new[] { "https://graph.microsoft.com/User.Read" }));


        }
    }
}
