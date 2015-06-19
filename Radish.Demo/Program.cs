using System;
using System.Threading;

namespace Radish.Demo
{
    public class Program
    {
        /// <summary>
        /// Indicates Ctrl+C was pressed
        /// </summary>
        private static AutoResetEvent exitSignal = new AutoResetEvent(false); 

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            RestServer server = new RestServer();

            try
            {
                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Unable to start REST server.\n" + 
                    "Make sure the application is running with Administrator privilegies." + 
                    "Exception details:\n\n" +
                    ex.ToString());
            }
            
            Console.WriteLine(
                "This is an example of using Radish for documenting your REST API methods.\n\n" +
                "Open your favorite web browser and type the following\n" + 
                "in the address bar to see the auto-generated documentation:\n\n" +
                "http://localhost:8040/api/help/simple    -- simple template set\n" +
                "http://localhost:8040/api/help/bootstrap -- Bootstrap-based template set\n" );
            
            exitSignal.WaitOne();
            server.Stop();
        }

        /// <summary>
        /// Handles Ctrl+C press in console mode
        /// </summary>
        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            exitSignal.Set();
            e.Cancel = true;
        }
    }
}
