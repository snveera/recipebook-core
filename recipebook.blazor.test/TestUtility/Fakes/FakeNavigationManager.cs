using Microsoft.AspNetCore.Components;
using System;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeNavigationManager:NavigationManager
    {
        private readonly TestContext _context;

        public FakeNavigationManager(TestContext context)
        {
            _context = context;
        }

        public FakeNavigationManager()
        {
            EnsureInitialized();
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            _context.NavigationLocations.Add(uri);
        }

        protected override void EnsureInitialized()
        {
            base.Initialize(new Uri("http://www.recipes.com").ToString(), new Uri("http://www.recipes.com").ToString());
        }
    }
}
