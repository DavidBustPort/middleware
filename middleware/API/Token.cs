using middleware.Models.API;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace middleware.API
{
    public static class Token
    {
        private const string URL_Authenticate = "http://keyquimica.vertrou.mx:81/corponet/api/v1/login/authenticate";

        public static string GetToken()
        {
            var token = "";
            var client = new HttpClient
            {
                BaseAddress = new Uri(URL_Authenticate)
            };

            var credentials = new Authentication
            {
                Username = "cliente",
                Password = "c0rp0n37"
            };

            var json = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = client.PostAsync(URL_Authenticate, data).Result;
                var result = JsonConvert.DeserializeObject<AuthenticationRes>(response.Content.ReadAsStringAsync().Result);
                Log.Write(result?.Token);
                token = result?.Token;
            }
            catch { }

            return token;
        }
    }
}
