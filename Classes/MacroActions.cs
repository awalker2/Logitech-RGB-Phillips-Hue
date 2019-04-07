using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LedCSharp;

/* References
   https://en.wikipedia.org/wiki/HSL_and_HSV#Color_conversion_formulae
 
*/

namespace LogitechRGBTest.Classes
{
    // For HSL to RGB% function
    enum RBGnVal
    {
        R = 0,
        G = 8,
        B = 4
    }

    class MacroActions
    {
        private static int initDelay = 100;
        private List<KeyValuePair<string, keyboardNames>> gKeyList;

        public MacroActions()
        {
            // Initialize the LED SDK
            bool LedInitialized = LogitechLEDGSDK.LogiLedInitWithName("logitech_rgb");
            Thread.Sleep(initDelay);
            if (!LedInitialized)
            {
                Console.WriteLine("LogitechGSDK.LogiLedInit() failed.");
            }
            Console.WriteLine("LED SDK Initialized");
            LogitechLEDGSDK.LogiLedSetTargetDevice(LogitechLEDGSDK.LOGI_DEVICETYPE_ALL);

            // Set all devices to Black
            LogitechLEDGSDK.LogiLedSetLighting(0, 0, 0);
            // Initialize list to keep track of G keys
            gKeyList = new List<KeyValuePair<string, keyboardNames>>();

            // Initialize the Macro SDK with callback implementation
            LogitechMacroGSDK.logiGkeyCB cbInstance = new LogitechMacroGSDK.logiGkeyCB((this.GkeySDKCallback));
            LogitechMacroGSDK.LogiGkeyInitWithoutContext(cbInstance);
        }

        ~MacroActions()
        {
            LogitechMacroGSDK.LogiGkeyShutdown();
            LogitechLEDGSDK.LogiLedShutdown();
        }

        private int HueHSLtoRGBPercent(double H, double S, double L, int n)
        {
            double a = S * Math.Min(L, 1 - L);
            double k = ((n + H / 30) % 12);
            double f = L - a * Math.Max(Math.Min(k - 3, Math.Min(9 -k, 1)), -1);

            return (int)Math.Round(f * 100);
        }

        public void GKeyMimicLightState(keyboardNames key, ref LightState state) 
        {
            if (state.on)
            {
                // Scale hue, sat, brightness
                double H = state.hue * (360.0 / 65535);
                double S = state.sat * (1.0 / 254);
                double L = state.bri * (1.0 / 254);

                LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(key, 
                    HueHSLtoRGBPercent(H, S, L, (int)RBGnVal.R), 
                    HueHSLtoRGBPercent(H, S, L, (int)RBGnVal.G), 
                    HueHSLtoRGBPercent(H, S, L, (int)RBGnVal.B));
            }
            else
            {
                LogitechLEDGSDK.LogiLedSetLightingForKeyWithKeyName(key, 0, 0, 0);
            }

            gKeyList.Add(new KeyValuePair<string, keyboardNames>(key.ToString(), key));
        }

        public void GkeySDKCallback(LogitechMacroGSDK.GkeyCode gKeyCode, String gKeyOrButtonString, IntPtr context)
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
                    Console.WriteLine("Keyboard pressed: " + gKeyOrButtonString + " " + gKeyCode.complete);
                }
            }
        }
    }
}
