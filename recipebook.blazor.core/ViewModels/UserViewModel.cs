using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazorclient.Application.Models;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class UserViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Name { get; private set; } = "";

        public ICollection<KeyValuePair<string,string>> Claims { get; private set; } = new List<KeyValuePair<string, string>>();

        public async Task Initialize()
        {
            var data = await _userService.Get();
            this.Name = data.Name;
            this.Claims = data?.Claims?.Select(Map)?.ToList();
        }

        private static KeyValuePair<string, string> Map(UserClaim claimData)
        {
            return new KeyValuePair<string, string>(claimData?.Type, claimData?.Value);
        }
    }
}