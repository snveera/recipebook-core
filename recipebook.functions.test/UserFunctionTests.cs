using recipebook.core.Models;
using recipebook.functions.Functions;
using recipebook.functions.test.TestUtility;
using recipebook.functions.test.TestUtility.Extensions;
using System;
using System.Threading.Tasks;
using recipebook.core.Managers;
using Xunit;

namespace recipebook.functions.test
{
    public class UserFunctionTests
    {
        [Fact]
        public async Task Run_UnAuthenticatedUser_ReturnsNoUserData()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithUnAuthenticatedUser();

            var api = root.Get<UserFunction>();

            // When
            var result = await api.Run(root.GetRequest(), root.CoreLogger());

            // Then
            var userResult = result.AssertIsOkResultWithValue<User>();

            Assert.Empty(userResult.Claims);
            Assert.Null(userResult.Name);
            Assert.False(userResult.IsAuthenticated);
        }

        [Fact]
        public async Task Run_AuthenticatedUser_ReturnsUserData()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithAuthenticatedUser("the-user");

            var api = root.Get<UserFunction>();

            // When
            var result = await api.Run(root.GetRequest(), root.CoreLogger());

            // Then
            var userResult = result.AssertIsOkResultWithValue<User>();

            Assert.Equal("the-user", userResult.Name);
            Assert.NotEmpty(userResult.Claims);
            Assert.True(userResult.IsAuthenticated);
        }
    }
}
