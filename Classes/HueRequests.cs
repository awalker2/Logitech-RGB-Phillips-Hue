using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/* References 
   https://stackoverflow.com/questions/9620278/how-do-i-make-calls-to-a-rest-api-using-c (Brian Swift)
   https://carldesouza.com/httpclient-getasync-postasync-sendasync-c/
*/
namespace LogitechRGBTest.Classes
{
    class LightState
    {
        public Boolean on = false;
        public int bri = 0;
        public int hue = 0;
        public int sat = 0;
    }

    class HueRequests
    {
        HttpClient client;
        private const String bridgeIP = "192.168.1.162";
        private const String userId = "OJpMrwAjN3QbSFqj127fL2vEeviz4g8nCtlIqoIC";

        public HueRequests()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~HueRequests()
        {
            client.Dispose();
        }

        public async Task<bool> LightOnAsync(String id)
        {
            var payload = "{\"on\": true}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PutAsync(
               $"http://{bridgeIP}/api/{userId}/lights/{id}/state", content);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public async Task<bool> LightOffAsync(String id)
        {
            var payload = "{\"on\": false}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PutAsync(
               $"http://{bridgeIP}/api/{userId}/lights/{id}/state", content);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public void ToggleBrightness(String id)
        {

        }

        public void ToggleColor(String id)
        {

        }

        public async Task<LightState> GetStateAsync(String id)
        {
            LightState state = new LightState();
            try
            {
                HttpResponseMessage response = client.GetAsync(
                $"http://{bridgeIP}/api/{userId}/lights/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    JObject lightJson = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    JObject stateJson = (JObject)lightJson.GetValue("state");
                    state.on = (bool)stateJson.GetValue("on");
                    state.bri = (int)stateJson.GetValue("bri");
                    state.hue = (int)stateJson.GetValue("hue");
                    state.sat = (int)stateJson.GetValue("sat");
                    return state;
                }
                else
                {
                    Console.WriteLine("HTTP Error: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
