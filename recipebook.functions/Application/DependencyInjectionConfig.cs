using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using recipebook.core.Managers;
using recipebook.core.Repositories;
using recipebook.entityframework;

namespace recipebook.functions.Application
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection builderServices)
        {
            builderServices.AddHttpContextAccessor();

            builderServices.AddTransient<CategoryManager>();
            builderServices.AddTransient<CategoryRepository>();

            builderServices.AddTransient<RecipeManager>();
            builderServices.AddTransient<RecipeRepository>();

            builderServices.AddTransient<HealthManager>();
            
            builderServices.AddTransient(c =>
            {
                var context = c.GetService<IHttpContextAccessor>();
                var principal = context?.HttpContext?.User;
                return new UserManager(principal);
            });

            builderServices.AddDbContext<RecipeBookDbContext>((provider, builder) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var accountEndpoint = configuration["RecipeBookDb:Endpoint"];
                var accountKey = configuration["RecipeBookDb:AccountKey"];
                var databaseName = configuration["RecipeBookDb:DatabaseName"];

                builder.UseCosmos(accountEndpoint, accountKey, databaseName);

            });

        }
    }
}
