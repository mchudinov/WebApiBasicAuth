using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiBasicAuth.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var tupleUserPass = ParseAuthorizationHeader(actionContext);
            if (!OnAuthenticateUser(tupleUserPass?.Item1, tupleUserPass?.Item2))
            {
                tupleUserPass = null;
            }

            if (null != tupleUserPass)
            {
                var basicIdentity = new BasicAuthenticationIdentity(tupleUserPass.Item1, tupleUserPass.Item2);
                var userIdentity = new GenericPrincipal(basicIdentity, null);
                SetPrincipal(userIdentity);
            }

            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        protected virtual bool OnAuthenticateUser(string username, string password)
        {
            bool retValue = false;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                retValue = true;
            }
            return retValue;
        }

        protected virtual Tuple<string, string> ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            var tokens = authHeader.Split(':');
            if (tokens.Length < 2)
                return null;

            return Tuple.Create<string, string>(tokens[0], tokens[1]);
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            this.Password = password;
        }
        public string Password { get; set; }
    }
}