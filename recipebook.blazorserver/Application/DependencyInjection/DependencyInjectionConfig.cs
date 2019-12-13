using Microsoft.Extensions.DependencyInjection;
using recipebook.blazor.core.Services;
using recipebook.blazor.core.ViewModels;

namespace recipebook.blazorserver.Application.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IUserService,UserService>();

            services.AddTransient<RecipeIndexViewModel>();
            services.AddTransient<RecipeDetailViewModel>();
            services.AddTransient<RecipeEditViewModel>();
            services.AddTransient<UserViewModel>();

            services.AddTransient<CategoryService>();
            services.AddSingleton<ICategoryService, CachedCategoryService>();
            
            services.AddTransient<IRecipeService,RecipeService>();
        }
    }
}