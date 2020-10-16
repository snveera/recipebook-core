using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using recipebook.blazor.Models;
using recipebook.blazor.Repositories;
using recipebook.blazor.Services;
using System;
using System.Net.Http;

namespace recipebook.blazor.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, string baseAddress)
        {
            RegisterDefaultHttpClient(services, baseAddress);
            RegisterApiHttpClient(services, configuration);

            services.AddTransient<ICategoryService,CategoryService>();
            services.AddTransient<IRecipeService,RecipeService>();

            services.AddSingleton<AppState>();

            services.AddSingleton<CategoryRepository>();
            services.AddTransient<RecipeRepository>();

        }

        private static void RegisterDefaultHttpClient(IServiceCollection services, string baseAddress)
        {
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
        }

        private static void RegisterApiHttpClient(IServiceCollection services, IConfiguration configuration)
        {
            var baseAddress = configuration["ApiBaseUri"];
            var permission = configuration["ApiPermission"];

            services.AddHttpClient("RecipeApi", client =>
                client.BaseAddress = new Uri(baseAddress));

            services.AddHttpClient("RecipeApiAuthenticated", client =>
               client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                .ConfigureHandler(new[] { baseAddress }, new[] { permission }));
        }


    }
}
