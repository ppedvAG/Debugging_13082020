using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloTrace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Start ****");
            //Console.WriteLine("press key to continue");
            //Console.ReadKey();

            //Debug.Assert(DateTime.Now.DayOfWeek == DayOfWeek.Friday);
            //Debugger.Break();

            //Trace.Listeners.Add(new ConsoleTraceListener());
            //Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;

            Debug.WriteLine("Debug Log started...");
            Trace.WriteLine("Trace started","greeting");

#if DEBUG
            Console.WriteLine("DEBUG Version !!!");
#else
            Console.WriteLine("RELEASE Version!!!!");
#endif

#if WURST
            Console.WriteLine("Wurst version");
#endif


            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
