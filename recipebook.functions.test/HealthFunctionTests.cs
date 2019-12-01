using System;
using System.Threading.Tasks;
using recipebook.core.Models;
using recipebook.functions.Functions;
using recipebook.functions.test.TestUtility;
using recipebook.functions.test.TestUtility.Extensions;
using Xunit;

namespace recipebook.functions.test
{
    public class HealthFunctionTests
    {
        [Fact]
        public async Task Run_NoInputs_ReturnsApplicationData()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var api = root.Get<HealthFunction>();

            // When
            var result = await api.Run(root.GetRequest(), root.CoreLogger());

            // Then
            var healthResult = result.AssertIsOkResultWithValue<ApplicationHealth>();

            Assert.True(!string.IsNullOrWhiteSpace(healthResult.Version));
            Assert.InRange(healthResult.CurrentDateTime, DateTime.Today, DateTime.Today.AddDays(1));

            Assert.True(healthResult.Status);
            Assert.NotEmpty(healthResult.DependencyStatus);
        }
    }
}