using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.core.Models;
using recipebook.functions.Functions;
using recipebook.functions.test.TestUtility;
using recipebook.functions.test.TestUtility.Extensions;
using Xunit;

namespace recipebook.functions.test
{
    public class CategoryFunctionTests
    {
        [Fact]
        public async Task Run_GetRequest_ReturnsAllCategories()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithCategory("category-one");
            root.WithCategory("category-two");

            var function = root.Get<CategoryFunction>();

            // When
            var result = await function.Run(root.GetRequest(), root.CoreLogger());

            //Then
            var resultData = result.AssertIsOkResultWithValue<IReadOnlyCollection<Category>>();

            Assert.NotEmpty(resultData);
            Assert.Contains("category-one",resultData.Select(r=>r.Name));
            Assert.Contains("category-two",resultData.Select(r=>r.Name));
        }
    }
}