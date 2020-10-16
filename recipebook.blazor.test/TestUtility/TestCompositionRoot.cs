using System.Collections.Generic;
using System.Linq;
using Bunit;
using Bunit.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using recipebook.blazor.Models;
using recipebook.blazor.Configuration;
using recipebook.blazor.test.TestUtility.Extensions;
using recipebook.blazor.Services;
using recipebook.blazor.test.TestUtility.Fakes;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using static Bunit.ComponentParameterFactory;
using System.Threading.Tasks;
using Bunit.Extensions;
using Bunit.TestDoubles.Authorization;

namespace recipebook.blazor.test.TestUtility
{
    public class TestCompositionRoot
    {
        private readonly Bunit.TestContext _hostContext;

        public static TestCompositionRoot Create()
        {
            var host = new Bunit.TestContext();
            return new TestCompositionRoot(host, new ConfigurationBuilder());
        }

        public TestCompositionRoot(Bunit.TestContext hostContext, ConfigurationBuilder configurationBuilder)
        {
            _hostContext = hostContext;
            var configuration = configurationBuilder.Build();
            DependencyInjectionConfig.Register(_hostContext.Services, configuration, "base-address");

            _hostContext.Services.AddTestAuthorization();
            RegisterFakes(_hostContext.Services);
        }

        private void RegisterFakes(IServiceCollection services)
        {
            services.AddSingleton<TestContext>();

            services.ReplaceTransient<ICategoryService, FakeCategoryService>();
            services.ReplaceTransient<IRecipeService, FakeRecipeService>();
            services.ReplaceTransient<NavigationManager, FakeNavigationManager>();
        }

        public TestContext TestContext => _hostContext.Services.GetService<TestContext>();

        public IRenderedComponent<TComponent> GetComponent<TComponent>(Dictionary<string, object> parameters = null) 
            where TComponent : IComponent
        {
            var componentParameters = (parameters ?? new Dictionary<string,object>())
                .Select(p => ComponentParameter.CreateParameter(p.Key, p.Value))
                .ToList();
            
            componentParameters.Add(CascadingValue(Task.FromResult(new AuthenticationState(TestContext.AuthenticatedUser))));

            return _hostContext.RenderComponent<TComponent>(componentParameters.ToArray());

        }

        public T Get<T>()
        {
            return _hostContext.Services.GetService<T>();
        }
    }

    public class TestContext
    {
        public ClaimsPrincipal AuthenticatedUser { get; internal set; } = new ClaimsPrincipalFake("the-user", isAuthenticated: false);
        public List<string> NavigationLocations { get; set; } = new List<string>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
