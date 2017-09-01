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
        public HttpResponseMessage Hello(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, $"Hello! id={id}");
        }
    }
}
