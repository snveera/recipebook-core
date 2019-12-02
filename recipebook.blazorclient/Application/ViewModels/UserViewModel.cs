using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class UserViewModel
    {
        
        public string Name { get; private set; }

        public Dictionary<string,string> Claims { get; private set; }

        public async Task Initialize()
        {
            Name = "the-user";
            Claims = new Dictionary<string, string>
            {
                {"one", "one-value"},
                {"two", "two-value"},
            };
        }
    }
}