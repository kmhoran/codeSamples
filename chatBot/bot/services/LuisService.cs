using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using QuoteMuleBot1.Models;

namespace QuoteMuleBot1.Services
{
    public class LuisService
    {
        public static async Task<LuisObject> ParseFaqInput(string inputStr)
        {
            LuisObject _Data = null;
            string response = String.Empty;
            string strEscaped = Uri.EscapeDataString(inputStr);

            using (var client = new HttpClient())
            {
                string uri = String.Format("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/...q={0}", strEscaped);
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    string jsonResponse = await msg.Content.ReadAsStringAsync();
                    _Data = JsonConvert.DeserializeObject<LuisObject>(jsonResponse);
                }

                return _Data;

            }
        }




        public static async Task<LuisObject> ParseDirectAddressInput(string inputStr)
        {
            LuisObject _Data = null;
            string response = String.Empty;
            string strEscaped = Uri.EscapeDataString(inputStr);

            using (var client = new HttpClient())
            {
                
                string uri = String.Format("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/...q={0}", strEscaped);

                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    string jsonResponse = await msg.Content.ReadAsStringAsync();
                    _Data = JsonConvert.DeserializeObject<LuisObject>(jsonResponse);
                }

                return _Data;

            }
        }

    }
}