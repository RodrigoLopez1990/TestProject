using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WebApiProject
{
    public class Dolar : IDivisa
    {
        public object GetPrice()
        {
            InitializeConnection();
            var result = GetTodayPrice();
            return result;
        }

        private HttpClient httpClient;
        private void InitializeConnection()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.bancoprovincia.com.ar");
        }

        private List<string> GetTodayPrice()
        {
            HttpResponseMessage reponse = httpClient.GetAsync("principal/dolar").Result;
            if (reponse.IsSuccessStatusCode)
            {
                return reponse.Content.ReadAsAsync<List<string>>().Result;
            }
            else
                return null;
        }
    }
}