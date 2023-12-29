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
using System.IO;

namespace FEI_Tactics.Services
{
    public class MatchMakingService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<MatchMakingResponse> SolicitarPartidaAsync(string gamerTag)
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
                    await Task.Delay(10000);
                    Debug.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var responseObj = JsonConvert.DeserializeObject<MatchMakingResponse>(responseJson);
                            return responseObj;
                        } catch (JsonSerializationException)
                        {
                            var responseObj = JsonConvert.DeserializeAnonymousType(responseJson, new { Respuesta = "", Gamertag = "" });
                            return new MatchMakingResponse(responseObj.Respuesta, responseObj.Gamertag);
                        }
                    } else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeAnonymousType(responseJson, new { Respuesta = "", Gamertag = "" });
                        return new MatchMakingResponse(responseObj.Respuesta, responseObj.Gamertag);
                    } else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            } catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
        }

        public static async Task<string> CancelarBusquedaAsync(string gamerTag)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"{URL_API}matchmaking/cancelarbusqueda");
                request.Method = "PATCH";
                request.ContentType = "application/json";

                var requestData = new
                {
                    Gamertag = gamerTag
                };

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (var response = await request.GetResponseAsync())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseJson = await streamReader.ReadToEndAsync();
                    var responseObject = JsonConvert.DeserializeAnonymousType(responseJson, new { Respuesta = "" });
                    return responseObject.Respuesta;
                }
            } catch (Exception ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
        }

        //TEST
        public static async Task<string> CancelarPartidaAsync(string gamerTag)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"{URL_API}matchmaking/cancelarpartida");
                request.Method = "PATCH";
                request.ContentType = "application/json";

                var requestData = new
                {
                    Gamertag = gamerTag
                };

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (var response = await request.GetResponseAsync())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseJson = await streamReader.ReadToEndAsync();
                    var responseObject = JsonConvert.DeserializeAnonymousType(responseJson, new { Respuesta = "" });
                    return responseObject.Respuesta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
        }
    }
}
