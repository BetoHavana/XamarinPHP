using CarsAppAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsAppAPI.Service
{
    /* VAMOS A OMITIR ESTA CLASE, TODAS LAS LLAMADAS SE HARAN DESDE MainViewModel
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * ****************************************************************************************************/
    public class WebApiClientService
    {
        //Uri urlBase = new Uri("http://192.168.100.8");
        //Uri urlBase = new Uri("http://ruffsstudios.com");
        //string requestUri = "/Unity/?";  ->for every method, not global
        //string requestUri = "/server/?";
        Uri urlBase = new Uri("https://ruffsstudios.com/api_carros/public");

        public async Task<T> executeRequestPostLogin<T>(object objectParams)
        {
            string requestUri = "/api/v1/auth/login";
            var client = new HttpClient();
            client.BaseAddress = urlBase;
            string jsonData = JsonConvert.SerializeObject(objectParams);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("content " + content);
            Console.WriteLine("Body" + jsonData.ToString());
            HttpResponseMessage response = await client.PostAsync(urlBase + requestUri, content);
            Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST 2 " + json);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public async Task executeRequestListCars()
        {
            string requestUri = "/api/v1/cars";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvcnVmZnNzdHVkaW9z" +
                "LmNvbVwvYXBpX2NhcnJvc1wvcHVibGljXC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyMjI2MDk2MywiZX" +
                "hwIjoxNjIyNDc2OTYzLCJuYmYiOjE2MjIyNjA5NjMsImp0aSI6IkM2YTRlVUlmc3NSUFEwSTkiLCJzdWIiOjEsInByd" +
                "iI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk" +
                "3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.r8KPFQo4GXZhsgk7fhGU_KDBZYOnZ0FMzDoKAcTU2LY";

            HttpClient cliente = new HttpClient();
            //cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await cliente.GetAsync(urlBase + requestUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("List of cars: " + json);
                //return JsonConvert.DeserializeObject<AutoData>(json);
            }
            
        }
        public async Task<T> executeRequestGetAuto<T>(string stParams)
        {
            string requestUri = "/api/v1/cars";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9ydWZmc3N0dWRpb3MuY29tXC9hcGlfY2Fycm9zXC9wdWJsaWNcL2FwaVwvdjFcL2F1dGhcL2xvZ2luIiwiaWF0IjoxNjIyMjQzMjE4LCJleHAiOjE2MjI0NTkyMTgsIm5iZiI6MTYyMjI0MzIxOCwianRpIjoic0d5enhkYmhHTk9HUXRXQiIsInN1YiI6MywicHJ2IjoiODdlMGFmMWVmOWZkMTU4MTJmZGVjOTcxNTNhMTRlMGIwNDc1NDZhYSJ9.1gMUWUTr-cTGYpFlOHzlO1siwnNPmfIRg6lFHeVmAaU";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.BaseAddress = urlBase;
            Console.WriteLine("Adress " + client.BaseAddress);
            HttpResponseMessage response = await client.GetAsync($"{requestUri}");
            //response.EnsureSuccessStatusCode();
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
