using System.Threading.Tasks;
using recipebook.blazor.core.ViewModels;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using Xunit;

namespace recipebook.blazor.test.ViewModels
{
    public class RecipeEditViewModelTests
    {
        [Fact]
        public void NewInstance_NoParameters_ShowsCreateRecipe()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("one");

            // When
            var viewModel = root.Get<RecipeEditViewModel>();

            // Then
            Assert.NotNull(viewModel);
            Assert.Null(viewModel.Name);
            Assert.Null(viewModel.Id);
            Assert.Null(viewModel.Ingredients);
            Assert.Null(viewModel.Directions);
            Assert.Null(viewModel.Servings);
            Assert.Null(viewModel.Rating);
            Assert.Null(viewModel.Source);
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

            var viewModel = root.Get<RecipeEditViewModel>();

            // When
            await viewModel.Initialize("id-1");

            // Then
            Assert.Equal("my-recipe", viewModel.Name);
            Assert.Equal("id-1",viewModel.Id);
            Assert.Equal("item1\r\nitem2 ", viewModel.Ingredients);
            Assert.Equal("step one\r\nstep two", viewModel.Directions);
            Assert.Equal(5, viewModel.Servings);
            Assert.Equal(3,viewModel.Rating);
            Assert.Equal("somewhere out there", viewModel.Source);

        }

        [Fact]
        public async Task Initialize_NoMatchingRecipe_ShowsCreateRecipe()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<RecipeEditViewModel>();

            // When
            await viewModel.Initialize("id-1");

            // Then
            Assert.Equal("Not Found", viewModel.Name);
            Assert.Equal("id-1", viewModel.Id);
            Assert.Null(viewModel.Ingredients);
            Assert.Null(viewModel.Directions);
            Assert.Null(viewModel.Servings);
            Assert.Null(viewModel.Rating);
            Assert.Null(viewModel.Source);

        }
    }
}