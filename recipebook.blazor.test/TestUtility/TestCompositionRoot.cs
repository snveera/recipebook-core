﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using recipebook.blazor.test.TestUtility.Extensions;
using recipebook.blazor.test.TestUtility.Fakes;
using recipebook.blazorclient.Application.DependencyInjection;
using recipebook.blazorclient.Application.Models;
using recipebook.blazorclient.Application.Services;

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
        }

        public T Get<T>()
        {
            return _provider.GetService<T>();
        }
    }

    public class TestContext
    {
        public User CurrentUser { get; set; }
        public List<Category> CategoriesInApi { get; set; }
        public List<Recipe> RecipesInApi { get; set; }
    }
}