using recipebook.blazor.Pages.Recipes;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace recipebook.blazor.test.Pages.Recipes
{
    public class DetailTests
    {
        [Fact]
        public void Render_ValidRecipe_ShowsRecipe()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithRecipe("recipe-one", 
                id: "my-identifier",
                category:"category-one",
                servings:1,
                ingredients:"some food",
                directions:"what to do",
                source:"where it came from");

            // When
            var page = root.GetComponent<Detail>(new Dictionary<string, object> { { "RecipeId", "my-identifier" } });

            // Then
            Assert.Equal("my-identifier", page.Instance.Data.Id);
            Assert.Equal("recipe-one", page.Instance.Data.Name);
            Assert.Equal("category-one", page.Instance.Data.Category);
            Assert.Equal(1, page.Instance.Data.Servings);
            Assert.Equal("some food", page.Instance.Data.IngredientsRaw);
            Assert.Equal("what to do", page.Instance.Data.DirectionsRaw);
            Assert.Equal("where it came from", page.Instance.Data.Source);

        }

        [Fact]
        public void Render_InvalidRecipe_ShowsNotFound()
        {
            // Given
            var root = TestCompositionRoot.Create();

            // When
            var page = root.GetComponent<Detail>(new Dictionary<string, object> { { "RecipeId", "my-identifier" } });

            // Then
            Assert.Null(page.Instance.Data.Id);
            Assert.Equal("Not Found", page.Instance.Data.Name);

        }
    }
}
