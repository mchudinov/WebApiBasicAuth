using System.Web.Mvc;

namespace WebApiBasicAuth.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public string Index()
        {
            return "Unauthorized. Login please.";
        }
    }
}