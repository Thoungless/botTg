using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace botseeds.Models
{
    public class HttpRequests
    {
        public string BaseAddress = "https://money.yandex.ru/";

        public async Task<RootObject> GetHistory(DateTime dt)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {              

                httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer (твой яндекс API)"); // Яндекс апи
                var response = await httpClient.PostAsync(BaseAddress + "api/operation-history", new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("type","deposition"),
                new KeyValuePair<string, string>("records","5")
            }));
                var answer = JsonConvert.DeserializeObject<RootObject>(await response.Content.ReadAsStringAsync());
                    return answer;
                };
            }
            catch(Exception ex)
            {
                Exception d = ex;
                return new RootObject();
            }
        }
    }
}