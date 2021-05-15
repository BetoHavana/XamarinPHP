using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

            HttpResponseMessage response = await client.GetAsync(requestUri+stParams);

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
                Console.WriteLine("GET " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<T> executeRequestPost<T>(object objectParams)
        {
            string requestUri = "/Unity/?";
            //string requestUri = "/server/?";

            var client = new HttpClient();
            client.BaseAddress = urlBase;

            string jsonData = JsonConvert.SerializeObject(objectParams);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUri, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }
        public async Task<T> executeRequestDelete<T>(String stParams)
        {
            string requestUri = "/Unity/?";
            //string requestUri = "/server/?";
            var client = new HttpClient();
            client.BaseAddress = urlBase;

            HttpResponseMessage response = await client.DeleteAsync($"{requestUri}{stParams}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("DELETE " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }
        public async Task<T> executeRequestPut<T>(object objectParams)
        {
            string requestUri = "/Unity/?";
            //string requestUri = "/server/?";

            var client = new HttpClient();
            client.BaseAddress = urlBase;

            string jsonData = JsonConvert.SerializeObject(objectParams);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(requestUri, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("PUT " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }
    }
}
