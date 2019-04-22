using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedCSharp;
using LogitechRGBTest.Classes;

namespace Logi_SetTargetZone_Sample_CS
{
    public class Program
    {
        // Nightstand light ID: 3
        // Dresser light ID: 4
        // Desk RGB strip ID: 5

        static async Task Main(string[] args)
        {
            HueRequests requests = new HueRequests();
            MacroActions gActions = new MacroActions(requests);

            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();
            Console.WriteLine("LED SDK Shutting down");
        }
    }
}
