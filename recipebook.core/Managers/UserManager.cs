using recipebook.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace recipebook.core.Managers
{
    public class UserManager
    {
        private readonly ClaimsPrincipal _currentPrincipal;

        public UserManager(ClaimsPrincipal currentPrincipal)
        {
            _currentPrincipal = currentPrincipal;
        }

        public User GetCurrent()
        {
            var user = Map(_currentPrincipal);
            return user;
        }

        public User Get(ClaimsPrincipal principal)
        {
            var user = Map(principal);
            return user;
        }

        private User Map(ClaimsPrincipal principal)
        {
            var user = new User
            {
                Name = principal?.Identity.Name,
                IsAuthenticated = principal?.Identity?.IsAuthenticated ?? false,
                Claims = Map(principal?.Claims)
            };

            return user;
        }

        private IReadOnlyCollection<UserClaim> Map(IEnumerable<Claim> claims)
        {
            if (claims == null)
                return new List<UserClaim>();

            var userClaims = claims
                .AsParallel()
                .Select(Map)
                .ToList();

            return userClaims;
        }

        private static UserClaim Map(Claim c)
        {
            return new UserClaim
            {
                Issuer = c.Issuer,
                Type = c.Type,
                Value = c.Value,
                Properties = c.Properties ?? new Dictionary<string, string>()
            };
        }
    }
}
