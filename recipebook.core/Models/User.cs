using System;
using System.Collections.Generic;
using System.Text;

namespace recipebook.core.Models
{
    public class User
    {
        public string Name { get; set; }

        public IReadOnlyCollection<UserClaim> Claims { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    public class UserClaim
    {
        public string Issuer { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public IDictionary<string, string> Properties { get; set; }
    }
}
