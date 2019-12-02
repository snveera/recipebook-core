using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using recipebook.blazorclient.Application.DependencyInjection;

namespace recipebook.blazorclient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfig.Configure(services);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
