using Microsoft.Extensions.Logging;
using recipebook.functions.test.TestUtility.Fakes;

namespace recipebook.functions.test.TestUtility.Extensions
{
    public static class LoggingExtensions
    {
        public static ILogger CoreLogger(this TestCompositionRoot root)
        {
            return new FakeLogger<object>(root.Context);
        }
    }
}
