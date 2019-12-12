using System.Threading.Tasks;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeUserService : IUserService
    {
        private readonly TestContext _context;

        public FakeUserService(TestContext context)
        {
            _context = context;
        }
        public async Task<User> Get()
        {
            return _context.CurrentUser;
        }
    }
}