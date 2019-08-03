using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using URLCollector.Proxy;

namespace URLCollector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ILogger Logger = LoggerFactory.Get();
            //var logProperties = new Dictionary<String, String>(2);
            //logProperties["JobId"] = job.JobId;
            //Logger.SetLogProperty(logProperties); //Rename log file name;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
