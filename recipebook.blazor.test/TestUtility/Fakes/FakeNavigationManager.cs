using Microsoft.AspNetCore.Components;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeNavigationManager:NavigationManager
    {
        private readonly TestContext _context;

        public FakeNavigationManager(TestContext context)
        {
            _context = context;
        }
        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            
        }
    }
}