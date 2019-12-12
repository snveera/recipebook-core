using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using recipebook.blazorclient.Application.ViewModels;
using Xunit;

namespace recipebook.blazor.test.ViewModels
{
    public class RecipeIndexViewModelTests
    {
        [Fact]
        public void NewInstance_NoParameters_ShowsEmptyData()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");

            // When
            var viewModel = root.Get<RecipeIndexViewModel>();

            // Then
            Assert.NotNull(viewModel);
            Assert.Empty(viewModel.Categories);
            Assert.Empty(viewModel.Recipes);

            Assert.Equal("",viewModel.SearchTerms);
            Assert.Equal("",viewModel.Category);

            Assert.Equal("disabled", viewModel.CategoryStatus);
            Assert.Equal("", viewModel.RecipeSearchStatus);
        }

        [Fact]
        public async Task Initialize_NoParameters_GetsCategories()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");
            root.WithCategory("two");

            var viewModel = root.Get<RecipeIndexViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Equal(new[]{"one","two"},viewModel.Categories.Select(c=>c.Name));
            AssertCategoryUrl(viewModel.Categories);
            Assert.Equal("", viewModel.CategoryStatus);
            Assert.Equal("", viewModel.Category);

            Assert.Equal("", viewModel.RecipeSearchStatus);
            Assert.Empty(viewModel.Recipes);

            Assert.Equal("", viewModel.SearchTerms);
  
        }

        [Fact]
        public async Task InitializeParameters_WithCategory_GetsRecipes()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");
            root.WithCategory("two");

            root.WithRecipe("recipe-one");
            root.WithRecipe("recipe-two");

            var viewModel = root.Get<RecipeIndexViewModel>();

            // When
            await viewModel.InitializeParameters("one");

            // Then
            Assert.Equal("", viewModel.RecipeSearchStatus);
            Assert.Equal(new[] { "recipe-one", "recipe-two" }, viewModel.Recipes.Select(c => c.Name));

            Assert.Equal("", viewModel.SearchTerms);

            Assert.Equal("one",root.Context.RecipeCategorySentToApi);
        }

        [Fact]
        public async Task Search_SearchText_GetsRecipes()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");
            root.WithCategory("two");

            root.WithRecipe("recipe-one");
            root.WithRecipe("recipe-two");

            var viewModel = root.Get<RecipeIndexViewModel>();

            // When
            viewModel.SearchTerms = "what to look for";
            await viewModel.Search();

            // Then
            Assert.Equal("", viewModel.RecipeSearchStatus);
            Assert.Equal(new[] { "recipe-one", "recipe-two" }, viewModel.Recipes.Select(c => c.Name));
            AssertRecipeUrl(viewModel.Recipes);

            Assert.Equal("what to look for", root.Context.RecipeCriteriaSentToApi);
            Assert.Equal("", root.Context.RecipeCategorySentToApi);
        }

        private void AssertCategoryUrl(List<CategoryViewModel> categories)
        {
            foreach (var item in categories)
            {
                var expectedUrl = $"/category/{item.Name}";
                Assert.Equal(expectedUrl,item.Url);
            }
        }

        private void AssertRecipeUrl(List<RecipeViewModel> recipes)
        {
            foreach (var item in recipes)
            {
                var expectedUrl = $"/recipe/{item.Id}";
                Assert.Equal(expectedUrl, item.Url);
            }
        }
    }
}