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
        static void Main(string[] args)
        {
            MacroActions actions = new MacroActions();

            // Set all devices to Black
            LogitechLEDGSDK.LogiLedSetLighting(0, 0, 0);

            // Set some keys on keyboard
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.L, 0, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.O, 0, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.G, 0, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.I, 0, 100, 100);


            // Set G633/G933 headset logos to White, backsides to Purple
            LogitechLEDGSDK.LogiLedSetLightingForTargetZone(DeviceType.Headset, 0, 100, 100, 100);

            LogitechLEDGSDK.LogiLedSetLightingForTargetZone(DeviceType.Headset, 1, 100, 0, 100);

            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();

            // Set some keys on keyboard
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.L, 100, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.O, 100, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.G, 100, 100, 100);
            LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardNames.I, 100, 100, 100);


            Console.WriteLine("Press \"ENTER\" to continue...");
            Console.ReadLine();


            Console.WriteLine("LED SDK Shutting down");
        }
    }
}
