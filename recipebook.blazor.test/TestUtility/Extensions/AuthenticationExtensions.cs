using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using recipebook.blazor.test.TestUtility.Fakes;

namespace recipebook.blazor.test.TestUtility.Extensions
{
    public static class AuthenticationExtensions
    {
        public static TestCompositionRoot WithAuthenticatedUser(
            this TestCompositionRoot root,
            string name,
            string[] roleClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,name)
            };

            if (roleClaims?.Any() ?? false)
            {
                var givenClaims = roleClaims.Select(r => new Claim(ClaimTypes.Role, r));
                claims.AddRange(givenClaims);
            }

            var claimsIdentity = new ClaimsIdentity(claims, "some-auth-mechanism");

            root.TestContext.AuthenticatedUser = new ClaimsPrincipal(claimsIdentity);
            return root;
        }

        public static TestCompositionRoot WithUnAuthenticatedUser(
            this TestCompositionRoot root)
        {
            root.TestContext.AuthenticatedUser = new ClaimsPrincipalFake("the-user", isAuthenticated: false);
            return root;
        }
    }
}
