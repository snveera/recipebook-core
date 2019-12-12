using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;
using recipebook.blazor.test.TestUtility.Extensions;
using recipebook.blazor.test.TestUtility.Fakes;
using recipebook.blazorclient.Application.DependencyInjection;

namespace recipebook.blazor.test.TestUtility
{
    public class TestCompositionRoot
    {

        private readonly ServiceProvider _provider;

        public static TestCompositionRoot Create()
        {
            return new TestCompositionRoot(new ServiceCollection());
        }

        private TestCompositionRoot(IServiceCollection services)
        {

            DependencyInjectionConfig.Configure(services);
            RegisterFakes(services);

            _provider = services.BuildServiceProvider();
        }

        public TestContext Context => _provider.GetService<TestContext>();

        private static void RegisterFakes(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<TestContext>();

            serviceCollection.ReplaceTransient<IRecipeService, FakeRecipeService>();
            serviceCollection.ReplaceTransient<ICategoryService, FakeCategoryService>();
            serviceCollection.ReplaceTransient<IUserService, FakeUserService>();
        }

        public T Get<T>()
        {
            return _provider.GetService<T>();
        }
    }

    public class TestContext
    {
        public User CurrentUser { get; set; } = new User();
        public List<Category> CategoriesInApi { get; set; } = new List<Category>();
        public List<Recipe> RecipesInApi { get; set; } = new List<Recipe>();
        public string RecipeId { get; set; }
        public string RecipeCategorySentToApi { get; set; }
        public string RecipeCriteriaSentToApi { get; set; }
    }
}