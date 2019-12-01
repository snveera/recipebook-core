using System.Collections.Generic;
using System.Threading.Tasks;
using recipebook.core.Models;
using recipebook.functions.Functions;
using recipebook.functions.test.TestUtility;
using recipebook.functions.test.TestUtility.Extensions;
using Xunit;

namespace recipebook.functions.test
{
    public class RecipeFunctionTests
    {
        [Fact]
        public async Task GetAll_RecipesInDb_ReturnsAll()
        {
            // Given
            var root = TestCompositionRoot.Create();

            root.WithRecipe("recipe-one");
            root.WithRecipe("recipe-two");

            var api = root.Get<RecipeFunction>();

            // When
            var result = await api.GetAll(root.GetRequest(), root.CoreLogger());

            // Then
            var data = result.AssertIsOkResultWithValue<IReadOnlyList<Recipe>>();
            Assert.Equal(2,data.Count);
        }

        [Fact]
        public async Task Get_IdExists_ReturnsItem()
        {
            // Given
            var root = TestCompositionRoot.Create();

            root.WithRecipe("recipe-one");
            var recipe2 = root.WithRecipe("recipe-two");

            var api = root.Get<RecipeFunction>();

            // When
            var result = await api.GetItem(root.GetRequest(), recipe2.Id, root.CoreLogger());

            // Then
            var data = result.AssertIsOkResultWithValue<Recipe>();
            Assert.Equal("recipe-two", data.Name);
            Assert.False(string.IsNullOrWhiteSpace(data.Id));
        }
    }
}