using Microsoft.Extensions.DependencyInjection;
using recipebook.blazorclient.Application.Services;
using recipebook.blazorclient.Application.ViewModels;

namespace recipebook.blazorclient.Application.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<UserViewModel>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<UserService>();
        }
    }
}