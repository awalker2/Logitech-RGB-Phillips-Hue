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
            MacroActions gActions = new MacroActions();
            HueRequests requests = new HueRequests();

            LightState state1 = await requests.GetStateAsync("3");
            LightState state2 = await requests.GetStateAsync("4");

            if (state1 != null)
            {
                gActions.GKeyMimicLightState(keyboardNames.G_1, ref state1);
            }
            if (state2 != null)
            {
                gActions.GKeyMimicLightState(keyboardNames.G_2, ref state2);
            }

            await requests.LightOffAsync("3");
            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();
            await requests.LightOnAsync("3");

            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();
            Console.WriteLine("LED SDK Shutting down");
        }
    }
}
