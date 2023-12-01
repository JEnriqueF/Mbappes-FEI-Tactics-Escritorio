using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics
{
    public class APIClient
    {
        private string URL = "https://18n4qv9q-3000.usw3.devtunnels.ms/";

        public async Task<string> autenticarInicioSesionAsync(string gamertag, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new
                {
                    Gamertag = gamertag,
                    contrasenia = password
                };

                string jsonData = JsonConvert.SerializeObject(requestData);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{URL}jugador/iniciarsesion", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                } else
                {
                    throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
    }
}