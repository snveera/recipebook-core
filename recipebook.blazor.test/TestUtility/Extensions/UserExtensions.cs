using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using recipebook.blazorclient.Application.Models;

namespace recipebook.blazor.test.TestUtility.Extensions
{
    public static class UserExtensions
    {
        public static void WithUser(this TestCompositionRoot root, 
            string name,
            ICollection<KeyValuePair<string,string>> claims = null)
        {
            root.Context.CurrentUser.Name = name;
            root.Context.CurrentUser.Claims = claims?
                .Select(c => new UserClaim {Type = c.Key, Value = c.Value})?
                .ToList();
        }
    }
}