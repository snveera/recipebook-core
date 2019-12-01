using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace recipebook.functions.test.TestUtility.Extensions
{
    public static class AssertionExtensions
    {
        public static T AssertIsOkResultWithValue<T>(this IActionResult result)
        {
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var typedResult = (OkObjectResult)result;
            Assert.NotNull(typedResult.Value);
            Assert.IsAssignableFrom<T>(typedResult.Value);

            var objectValue = (T)typedResult.Value;
            return objectValue;
        }
    }
}
