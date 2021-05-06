using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGetRequestPhp.Service
{
    public class WebApiClientService
    {
        Uri urlBase = new Uri("http://192.168.100.8");
        //Uri urlBase = new Uri("http://ruffsstudios.com");

        public async Task<T> executeRequestGet<T>(string stParams)
        {
            string requestUri = "/Unity/?";
            //string requestUri = "/server/?";
            var client = new HttpClient();
            client.BaseAddress = urlBase;

            HttpResponseMessage response = await client.GetAsync($"{requestUri}{stParams}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("test "+json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<T> executeRequestGetAuto<T>(string stParams)
        {
            string requestUri = "/Unity/?";
            //string requestUri = "/server/?";
            var client = new HttpClient();
            client.BaseAddress = urlBase;

            HttpResponseMessage response = await client.GetAsync($"{requestUri}{stParams}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("test " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }


    }
}
