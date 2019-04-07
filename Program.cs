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
        static async Task Main(string[] args)
        {
            MacroActions actions = new MacroActions();
            HueRequests requests = new HueRequests();

            // Set all devices to Black
            LogitechLEDGSDK.LogiLedSetLighting(0, 0, 0);

            LightState state1 = requests.GetStateAsync("3").Result;
            LightState state2 = requests.GetStateAsync("4").Result;
            LightState state3 = requests.GetStateAsync("4").Result;

            if (state1 != null && state1.on == true)
            {
                LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.Q, 100, 100, 100);
            }
            if (state2 != null && state2.on == true)
            {
                LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.W, 100, 100, 100);
            }
            if (state2 != null && state3.on == true)
            {
                LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.E, 50, 50, 50);
            }

            requests.LightOff("3");
            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();
            requests.LightOn("3");

            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();
            Console.WriteLine("LED SDK Shutting down");
        }
    }
}
