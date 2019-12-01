using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using recipebook.functions;
using recipebook.entityframework;
using System.Security.Claims;
using System.Collections.Generic;
using recipebook.core.Managers;
using recipebook.functions.Application;
using recipebook.functions.test.TestUtility.Fakes;
using recipebook.functions.test.TestUtility.Extensions;
using recipebook.functions.Functions;

namespace recipebook.functions.test.TestUtility
{
    public class TestCompositionRoot
    {
       
        private readonly ServiceProvider _provider;
        private string _inMemoryDatabaseName;

        public static TestCompositionRoot Create()
        {
            return new TestCompositionRoot(Guid.NewGuid().ToString(), new ServiceCollection());
        }

        private TestCompositionRoot(string databaseName,
            IServiceCollection services)
        {
            _inMemoryDatabaseName = databaseName;

            DependencyInjectionConfig.Configure(services);
            RegisterFunctions(services);
            RegisterFakes(services);

            _provider = services.BuildServiceProvider();
        }

        public TestContext Context => _provider.GetService<TestContext>();

        private void RegisterFunctions(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HealthFunction>();
            serviceCollection.AddTransient<UserFunction>();
            serviceCollection.AddTransient<RecipeFunction>();
        }

        private void RegisterFakes(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<TestContext>();
            serviceCollection.ReplaceTransient<IHttpContextAccessor, FakeHttpContextAccessor>();
            
            serviceCollection.ReplaceTransient<ILogger<HealthManager>, FakeLogger<HealthManager>>();

            serviceCollection.ReplaceTransient(context =>
            {
                var options = new DbContextOptionsBuilder<RecipeBookDbContext>()
                    .UseInMemoryDatabase(databaseName: _inMemoryDatabaseName)
                    .Options;
                return new RecipeBookDbContext(options);
            });
        }

        public T Get<T>()
        {
            return _provider.GetService<T>();
        }
    }
    public class TestContext
    {
        public ClaimsPrincipal CurrentPrincipal { get; set; }
        public List<LoggedMessage> LoggedMessages { get; set; }
    }
}
