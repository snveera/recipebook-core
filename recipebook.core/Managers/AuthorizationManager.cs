using System.Security.Claims;

namespace recipebook.core.Managers
{
    public class AuthorizationManager
    {
        public bool CanManageRecipes(ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated ?? false;
        }

        public bool CanManageDatabase(ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated ?? false;
        }
    }
}
