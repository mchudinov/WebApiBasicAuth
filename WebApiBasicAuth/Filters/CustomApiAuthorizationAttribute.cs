using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;

namespace WebApiBasicAuth.Filters
{
    public class CustomApiAuthorizationAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool retValue = Thread.CurrentPrincipal.Identity.IsAuthenticated;

            if (retValue && Roles.Length > 0)
            {
                retValue = IsAutenticatedInRole(Thread.CurrentPrincipal);
            }

            if (retValue && Users.Length > 0)
            {
                retValue = IsAllowedUser(Thread.CurrentPrincipal.Identity?.Name);
            }

            return retValue;
        }

        private bool IsAllowedUser(string name)
        {
            if (Users.Length == 0)
                return false;
            var allowedUsers = Users.ToUpperInvariant().Split(',');
            return allowedUsers.Contains(name.ToUpperInvariant());
        }

        private bool IsAutenticatedInRole(IPrincipal principal)
        {
            var retValue = false;
            if (Roles.Length > 0)
            {
                var allowedRoles = Roles.Split(',');
                foreach (var role in allowedRoles)
                {
                    retValue |= principal.IsInRole(role);
                }                
            }
            return retValue;
        }
    }
}