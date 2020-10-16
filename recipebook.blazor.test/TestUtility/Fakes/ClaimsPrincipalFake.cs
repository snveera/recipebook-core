using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class ClaimsPrincipalFake : ClaimsPrincipal
    {
        private readonly List<Claim> _claims = new List<Claim>();

        public ClaimsPrincipalFake(string name, bool isAuthenticated = true) : base(new IdentityFake { Name = name, IsAuthenticated = isAuthenticated })
        {
            Identity = new IdentityFake { Name = name, IsAuthenticated = isAuthenticated };
            WithClaim(ClaimTypes.Name, name);
            WithClaim(ClaimTypes.Upn, $"{name}@somewhere.com");
        }

        public override IIdentity Identity { get; }

        public override IEnumerable<Claim> Claims => _claims;

        public void WithRoleClaims(IEnumerable<string> claims)
        {
            if (claims == null) return;

            foreach (var claimValue in claims)
            {
                _claims.Add(new Claim(ClaimTypes.Role, claimValue));

                foreach (var identity in Identities)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, claimValue));
                }
            }
        }

        public void WithClaims(Dictionary<string, string> dictionary)
        {
            if (dictionary == null) return;

            foreach (var entry in dictionary)
            {
                WithClaim(entry.Key, entry.Value);
            }
        }

        public void WithClaim(string claimType, string value)
        {
            _claims.Add(new Claim(claimType, value));
        }
    }

    public class IdentityFake : IIdentity
    {
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
