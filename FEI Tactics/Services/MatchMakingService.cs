using FEI_Tactics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FEI_Tactics.Services
{
    public class MatchMakingService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<string> SolicitarPartidaAsync(string gamerTag)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new
                    {
                        Gamertag = gamerTag
                    };
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{URL_API}matchmaking/solicitarpartida", content);
                                        
                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseJson);
                        string respuesta = jsonResponse["Respuesta"].ToString();

                        return respuesta;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseJson);
                        string respuesta = jsonResponse["Respuesta"].ToString();

                        return respuesta;
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
        }

    }
}
