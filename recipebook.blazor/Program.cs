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
using recipebook.blazor.Services;
using recipebook.blazor.Models;

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
            RegisterApiHttpClient(builder);

            RegisterServices(builder);

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

        private static void RegisterApiHttpClient(WebAssemblyHostBuilder builder)
        {
            var baseAddress = builder.Configuration["ApiBaseUri"];
            builder.Services.AddHttpClient("RecipeApi", client =>
                client.BaseAddress = new Uri(baseAddress));
        }

        private static void RegisterServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<CategoryService>();
            builder.Services.AddTransient<RecipeService>();

            builder.Services.AddSingleton<AppState>();
        }
    }
}
