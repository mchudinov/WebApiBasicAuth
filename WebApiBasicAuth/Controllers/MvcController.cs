using System.Web.Mvc;

namespace WebApiBasicAuth.Controllers
{
    public class MvcController : Controller
    {
        [Authorize]
        public string Index()
        {
            return "Hello world!";
        }
    }
}