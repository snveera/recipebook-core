using System.Security.Claims;

namespace recipebook.core.Managers
{
    public class AuthorizationManager
    {
        public bool CanManageRecipes(ClaimsPrincipal user)
        {
            return user != null;
        }
    }
}
