using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace Radish.Demo.Controllers
{
    public class HelpController : ApiController
    {
        [HttpGet]
        [Route("help/{ts}")]
        public IHttpActionResult GetHelp(string ts)
        {
            // Step 1. Create Documentor instance.
            Documentor documentor = new Documentor();

            // Step 2. Define methods groups.
            documentor.AddMethodGroup("persons", "Persons", "Describes methods to work with Person objects", 1);
            documentor.AddMethodGroup("pets", "Pets", "Describes methods to work with Pet objects", 2);
            
            // Step 3. Create template set according to requiested documentation output type (simple or Bootstrap-based).
            BasicTemplateSet templateSet = null;

            if (String.Equals(ts, "simple"))
                templateSet = new SimpleTemplateSet() { Title = "Radish Demo" };
            else if (String.Equals(ts, "bootstrap"))
                templateSet = new BootstrapTemplateSet() { Title = "Radish Demo" };

            // Step 4. Specify template set for the documentor.
            documentor.TemplateSet = templateSet;
            
            // Step 5. Get the help content.
            string helpContent = documentor.Content;

            HttpResponseMessage message = new HttpResponseMessage() { Content = new StringContent(helpContent, Encoding.UTF8, "text/html") };
            return new ResponseMessageResult(message);
        }
    }
}
