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
    public static class CartasService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<List<Carta>> RecuperarMazoAsync()
        {
            List<Carta> cartas = new List<Carta>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{URL_API}carta/recuperarcartas");

                    if (response.IsSuccessStatusCode)
                    {
                        /*string responseJson = await response.Content.ReadAsStringAsync();
                        mazo = JsonConvert.DeserializeObject<List<CartaResponse>>(responseJson);*/

                        string responseJson = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeAnonymousType(responseJson, new { cartas = new List<Carta>() });
                        cartas = responseObj.cartas;
                    } else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            } catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
            return cartas;
        }
    }
}
