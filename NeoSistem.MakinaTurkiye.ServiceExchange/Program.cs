using NeoSistem.MakinaTurkiye.ExchangeService;
using System.ServiceProcess;

namespace NeoSistem.MakinaTurkiye.ServiceExchange
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                  {
                new Exchange()
                  };
            ServiceBase.Run(ServicesToRun);

            //#if (!DEBUG) 
            //      ServiceBase[] ServicesToRun;
            //      ServicesToRun = new ServiceBase[] 
            //      { 
            //        new Exchange() 
            //      };
            //      ServiceBase.Run(ServicesToRun);
            //#else
            //      Exchange service = new Exchange();
            //      service.ConfigureAndStart();
            //      System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            //#endif

        }

    }
}
