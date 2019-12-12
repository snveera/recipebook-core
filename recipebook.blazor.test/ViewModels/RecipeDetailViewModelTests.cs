using System.Threading.Tasks;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using recipebook.blazorclient.Application.ViewModels;
using Xunit;

namespace recipebook.blazor.test.ViewModels
{
    public class RecipeDetailViewModelTests
    {
        [Fact]
        public void NewInstance_NoParameters_ShowsEmptyData()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");

            // When
            var viewModel = root.Get<RecipeDetailViewModel>();

            // Then
            Assert.NotNull(viewModel);
            Assert.Equal("Loading...", viewModel.Title);
            Assert.Null(viewModel.Id);
            Assert.Empty(viewModel.Ingredients);
            Assert.Empty(viewModel.Directions);
            Assert.Equal("",viewModel.Servings);
            Assert.Null(viewModel.Rating);
            Assert.Equal("",viewModel.Source);
        }

        [Fact]
        public async Task Initialize_MatchingRecipe_ShowsRecipeDetails()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithRecipe("my-recipe",
                id:"id-1",
                servings:5,
                rating:3,
                source:"somewhere out there",
                ingredients:"item1\r\nitem2 ",
                directions: "step one\r\nstep two");

            var viewModel = root.Get<RecipeDetailViewModel>();

            // When
            await viewModel.Initialize("id-1");

            // Then
            Assert.Equal("my-recipe", viewModel.Title);
            Assert.Equal("id-1",viewModel.Id);
            Assert.Equal(new[]{"item1","item2"},viewModel.Ingredients);
            Assert.Equal(new[]{"step one","step two"},viewModel.Directions);
            Assert.Equal("5", viewModel.Servings);
            Assert.Equal(3,viewModel.Rating);
            Assert.Equal("somewhere out there", viewModel.Source);

        }

        [Fact]
        public async Task Initialize_NoMatchingRecipe_ShowsNotFound()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<RecipeDetailViewModel>();

            // When
            await viewModel.Initialize("id-1");

            // Then
            Assert.Equal("Not Found", viewModel.Title);
            Assert.Equal("id-1", viewModel.Id);
            Assert.Empty(viewModel.Ingredients);
            Assert.Empty(viewModel.Directions);
            Assert.Equal("", viewModel.Servings);
            Assert.Null(viewModel.Rating);
            Assert.Equal("", viewModel.Source);

        }
    }
}