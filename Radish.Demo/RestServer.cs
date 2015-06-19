using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Radish
{
    /// <summary>
    /// Implements RESTful API initialization and starting http self-host server
    /// </summary>
    public class RestServer 
    {
        #region Private members

        /// <summary>
        /// Port value to start http server
        /// </summary>
        private int httpServerPort = 8040;

        /// <summary>
        /// Self-hosting HTTP server instance
        /// </summary>
        private HttpSelfHostServer httpServer = null;

        /// <summary>
        /// Self-hosting configuration
        /// </summary>
        private HttpSelfHostConfiguration httpHostConfig = null;

        #endregion Private Members

        #region Public Methods

        /// <summary>
        /// Starts RESTful API server
        /// </summary>
        public void Start()
        {
            httpHostConfig = new HttpSelfHostConfiguration(String.Format("http://localhost:{0}/", httpServerPort));

            // map routes
            httpHostConfig.Routes.MapHttpRoute("help", "api/help/{ts}", new { controller = "Help", ts = RouteParameter.Optional });
            httpHostConfig.Routes.MapHttpRoute("persons", "api/persons/{personId}", new { controller = "Persons", personId = RouteParameter.Optional });
            httpHostConfig.Routes.MapHttpRoute("pets", "api/pets/{petId}", new { controller = "Pets", petId = RouteParameter.Optional });
            httpHostConfig.Routes.MapHttpRoute("pets-for-person", "api/persons/{personId}/pets", new { controller = "Pets" });

            // set JSON formatter
            httpHostConfig.Formatters.Clear();
            httpHostConfig.Formatters.Add(new JsonMediaTypeFormatter());

            // start http self-host server
            httpServer = new HttpSelfHostServer(httpHostConfig);
            try
            {
                httpServer.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(
                     "Unable to start REST server.\nMake sure the application is running with Administrator privilegies.\nException details:\n\n{0}", 
                     ex.ToString()));
            }
        }

        /// <summary>
        /// Stops RESTful API server
        /// </summary>
        public void Stop()
        {
            httpServer.CloseAsync().Wait();
        }

        #endregion Public Methods
    }
}
