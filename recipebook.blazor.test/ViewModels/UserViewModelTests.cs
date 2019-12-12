using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazor.core.ViewModels;
using recipebook.blazor.test.TestUtility;
using recipebook.blazor.test.TestUtility.Extensions;
using Xunit;

namespace recipebook.blazor.test.ViewModels
{
    public class UserViewModelTests
    {
        [Fact]
        public async Task Initialize_NoParameters_ShowsUserData()
        {
            // Given
            var root = TestCompositionRoot.Create();
            var claims = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name-1","value-1"),
                new KeyValuePair<string, string>("name-2","value-2")
            };
            root.WithUser("the-user", claims);

            var viewModel = root.Get<UserViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Equal("the-user",viewModel.Name);
            Assert.Contains("name-1",viewModel.Claims.Select(c=>c.Key));
            Assert.Contains("name-2",viewModel.Claims.Select(c=>c.Key));

            Assert.Equal("value-1",viewModel.Claims.First(c=>c.Key == "name-1").Value);
            Assert.Equal("value-2",viewModel.Claims.First(c=>c.Key == "name-2").Value);
        }
    }
}