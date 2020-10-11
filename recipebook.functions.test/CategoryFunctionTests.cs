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
            var result = await function.Run(root.GetRequest(), root.CoreLogger(), root.AuthenticatedUser());

            //Then
            var resultData = result.AssertIsOkResultWithValue<IReadOnlyCollection<Category>>();

            Assert.NotEmpty(resultData);
            Assert.Contains("category-one",resultData.Select(r=>r.Name));
            Assert.Contains("category-two",resultData.Select(r=>r.Name));
        }

        [Fact]
        public async Task Create_NameInput_CreatesCategory()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var function = root.Get<CategoryFunction>();

            var data = new Category {Name = "one-to-add"};
            var request = root.PostRequest(data);

            // When
            var result = await function.Create(request, root.CoreLogger(), root.AuthenticatedUser());

            //Then
            var resultData = result.AssertIsOkResultWithValue<Category>();

            Assert.NotNull(resultData);
            Assert.Contains("one-to-add", resultData.Name);

        }
    }
}