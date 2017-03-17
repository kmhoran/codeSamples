using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using QuoteMuleBot1.Domain;
using QuoteMuleBot1.Models.Responses;

namespace QuoteMuleBot1.Services
{
    public class QuoteMuleService
    {
        public static async Task<FaqQueryDomain> ParseLuisKeywords(List<string> keywordList)
        {
            ItemResponse<FaqQueryDomain> _Data = null;
            string response = String.Empty;
            string strEscaped = BuildUriList(keywordList);

            using (var client = new HttpClient())
            {
                string uri = String.Format("http://quotemule.azurewebsites.net/api/faq/keyword/{0}", strEscaped);
                //string uri = String.Format("http://quotemule.dev/api/faq/keyword/{0}", strEscaped);
                //string uri = String.Format("http://localhost:1552/api/faq/keyword/{0}", strEscaped);

                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    string jsonResponse = await msg.Content.ReadAsStringAsync();
                    _Data = JsonConvert.DeserializeObject<ItemResponse<FaqQueryDomain>>(jsonResponse);
                }

                return _Data.Item;

            }
        }

        public static string BuildUriList(List<string> keywordList)
        {
            string resultStr = "?";

            StringBuilder builder = new StringBuilder();
            foreach (string kw in keywordList)
            {
                builder.Append("kws=" + kw).Append("&");
            }

            builder.Length--;

            resultStr = String.Concat(resultStr, builder.ToString());

            return resultStr;
        }
    }
}