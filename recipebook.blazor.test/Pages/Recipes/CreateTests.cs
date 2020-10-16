using recipebook.blazor.Pages.Recipes;
using recipebook.blazor.test.TestUtility;
using Xunit;

namespace recipebook.blazor.test.Pages.Recipes
{
    public class CreateTests
    {
        [Fact]
        public void Render_ShowsEmptyRecipe()
        {
            // Given
            var root = TestCompositionRoot.Create();

            // When
            var page = root.GetComponent<Create>();

            // Then
            Assert.Null(page.Instance.Data.Id);
            Assert.Null(page.Instance.Data.Name);
            Assert.Null(page.Instance.Data.Category);
            Assert.Null(page.Instance.Data.Servings);
            Assert.Null(page.Instance.Data.IngredientsRaw);
            Assert.Empty(page.Instance.Data.Ingredients);
            Assert.Null(page.Instance.Data.DirectionsRaw);
            Assert.Empty(page.Instance.Data.Directions);
            Assert.Null(page.Instance.Data.Source);

        }
    }
}
