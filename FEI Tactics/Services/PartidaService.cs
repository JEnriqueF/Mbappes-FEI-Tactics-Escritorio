using FEI_Tactics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Services
{
    public static class PartidaService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<PartidaResponse> MandarMovimientosAsync(string gamertag, List<Movimiento> listaMovimientos)
        {
            PartidaResponse respuestaMovimiento = new PartidaResponse();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new
                    {
                        Gamertag = gamertag,
                        Movimientos = listaMovimientos
                    };
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{URL_API}matchmaking/jugarturno", content);
                    await Task.Delay(10000);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        respuestaMovimiento = JsonConvert.DeserializeObject<PartidaResponse>(responseJson);
                    } else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        respuestaMovimiento = null;
                    } else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            } catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
            return respuestaMovimiento;
        }
    }
}
