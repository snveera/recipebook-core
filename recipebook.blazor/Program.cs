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
using recipebook.blazor.Repositories;

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
            RegisterApiHttpClient(builder);

            RegisterServices(builder);

            builder.Services.AddMsalAuthentication(options =>
            {
                var permission = builder.Configuration["ApiPermission"];

                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(permission);
            });

            await builder.Build().RunAsync();
        }

        private static void RegisterDefaultHttpClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        }

        private static void RegisterApiHttpClient(WebAssemblyHostBuilder builder)
        {
            var baseAddress = builder.Configuration["ApiBaseUri"];
            var permission = builder.Configuration["ApiPermission"];

            builder.Services.AddHttpClient("RecipeApi", client =>
                client.BaseAddress = new Uri(baseAddress));

            builder.Services.AddHttpClient("RecipeApiAuthenticated", client =>
               client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                .ConfigureHandler(new[] { baseAddress }, new[] { permission }));
        }

        private static void RegisterServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<CategoryService>();
            builder.Services.AddTransient<RecipeService>();

            builder.Services.AddSingleton<AppState>();

            builder.Services.AddSingleton<CategoryRepository>();
            builder.Services.AddTransient<RecipeRepository>();
        }
    }
}
