using recipebook.blazor.Models;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace recipebook.blazor.test.Components
{
    public class SearchTests
    {
        [Fact]
        public void Render_ShowsAllCategories()
        {
            // Given
            var root = TestCompositionRoot.Create()
                .WithCategory("one")
                .WithCategory("two");

            // When
            var page = root.GetComponent<recipebook.blazor.Components.Search>();

            // Then
            var categoryNames = page.Instance.Categories.Select(c => c.Name);
            Assert.Contains("one", categoryNames);
            Assert.Contains("two", categoryNames);
        }

        [Fact]
        public async Task Search_ShowsSearchResults()
        {
            // Given
            var root = TestCompositionRoot.Create()
                .WithRecipe("recipe one")
                .WithRecipe("recipe two");

            var page = root.GetComponent<recipebook.blazor.Components.Search>();

            // When
            await page.Instance.SearchAsync();

            // Then
            var appState = root.Get<AppState>();
            Assert.Equal(2, appState.SearchResults.Count);
            Assert.Equal(new List<string> { "recipe one", "recipe two" }, appState.SearchResults.Select(r=>r.Name).ToList());
        }
    }
}
