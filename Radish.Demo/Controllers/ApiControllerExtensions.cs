using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Radish.Demo.Controllers
{
    public static class ApiControllerExtensions
    {
        public static ResponseMessageResult NotFound(this ApiController controller, string message)
        {
            return new ResponseMessageResult(new System.Net.Http.HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound, Content = new StringContent(message) });
        }
    }
}
