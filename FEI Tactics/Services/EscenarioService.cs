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
    public static class EscenarioService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<List<Escenario>> RecuperarEscenariosAsync()
        {
            List<Escenario> escenarios = new List<Escenario>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{URL_API}escenario/recuperarescenarios");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeAnonymousType(responseJson, new { escenarios = new List<Escenario>() });
                        escenarios = responseObj.escenarios;
                    } else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            } catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
            return escenarios;
        }
    }
}
