using System.Web.Http;
using System.Web.Routing;

namespace WebApiBasicAuth
{
    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
    //https://stackoverflow.com/questions/20149750/unauthorised-webapi-call-returning-login-page-rather-than-401
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
