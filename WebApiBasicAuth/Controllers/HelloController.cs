using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBasicAuth.Filters;

namespace WebApiBasicAuth.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        [BasicAuthentication, Authorize]
        [CustomApiAuthorization(Roles = "Superuser, Administrator", Users = "ApiUser")]
        public HttpResponseMessage Hello(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, $"Hello! id={id}");
        }
    }
}
