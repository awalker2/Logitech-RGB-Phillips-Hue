using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedCSharp;

namespace LogitechRGBTest.Classes
{
    class MacroActions
    {
        public MacroActions()
        {
            // Initialize the LED SDK
            bool LedInitialized = LogitechLEDGSDK.LogiLedInitWithName("Test");
            if (!LedInitialized)
            {
                Console.WriteLine("LogitechGSDK.LogiLedInit() failed.");
            }
            Console.WriteLine("LED SDK Initialized");
            LogitechLEDGSDK.LogiLedSetTargetDevice(LogitechLEDGSDK.LOGI_DEVICETYPE_ALL);

            // Initialize the Macro SDK with callback implementation
            LogitechMacroGSDK.logiGkeyCB cbInstance = new LogitechMacroGSDK.logiGkeyCB((this.gkeySDKCallback));
            LogitechMacroGSDK.LogiGkeyInitWithoutContext(cbInstance);
        }

        public void gkeySDKCallback(LogitechMacroGSDK.GkeyCode gKeyCode, String gKeyOrButtonString, IntPtr context)
        {
            if (gKeyCode.keyDown == 0)
            {
                if (gKeyCode.mouse == 1)
                {
                    // Handle gkey released mouse
                }
                else
                {
                    // Handle gkey released on keyboard/headset
                }
            }
            else
            {
                if (gKeyCode.mouse == 1)
                {
                    // Handle gkey pressed on mouse
                }
                else
                {
                    // Handle gkey pressed on keyboard/headset
                    Console.WriteLine("Keyboard pressed: " + gKeyOrButtonString);
                }
            }
        }

        ~ MacroActions()
        {
            // Shutdown
            LogitechMacroGSDK.LogiGkeyShutdown();
            LogitechLEDGSDK.LogiLedShutdown();
        }
    }
}
