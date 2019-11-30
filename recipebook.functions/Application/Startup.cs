
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(recipebook.functions.Application.Startup))]

namespace recipebook.functions.Application
{
    public class Startup : FunctionsStartup
    {
        // Based on info from https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
        public override void Configure(IFunctionsHostBuilder builder)
        {
            DependencyInjectionConfig.Configure(builder.Services);
        }
    }
}
