using FEI_Tactics.Models;
using FEI_Tactics.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics
{
    public static class JugadorService
    {
        private const string URL_API = "https://mk2m8b3x-3000.usw3.devtunnels.ms/";

        public static async Task<JugadorResponse> AutenticarInicioSesionAsync(string gamertag, string password)
        {
            JugadorResponse jugadorActual = new JugadorResponse();

            try
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
                    HttpResponseMessage response = await client.PostAsync($"{URL_API}jugador/iniciarsesion", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        jugadorActual = JsonConvert.DeserializeObject<JugadorResponse>(responseJson);
                    } else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        jugadorActual = null;
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            } catch (HttpRequestException ex)
            {
                throw new Exception("Fallo en la conexión al servidor.", ex);
            }
            return jugadorActual;
        }

        public static async Task<Boolean> RegistrarCuentaJugadorAsync(string gamerTag, string contrasenia, int fotoPerfil)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new
                    {
                        gamertag = gamerTag,
                        password = contrasenia,
                        idFoto = fotoPerfil
                    };
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{URL_API}jugador/registrarjugador", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        return false;
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

        public static async Task<List<FotoPerfil>> RecuperarFotosPerfilAsync()
        {
            List<FotoPerfil> fotosPerfil = new List<FotoPerfil>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{URL_API}jugador/recuperarfotosperfil");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeAnonymousType(responseJson, new { imagenesPerfil = new List<FotoPerfil>() });
                        fotosPerfil = responseObj.imagenesPerfil;
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
            return fotosPerfil;
        }

        public static async Task<bool> ActualizarMazoAsync(string gamertag, string mazoNuevo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new
                    {
                        Gamertag = gamertag,
                        Mazo = mazoNuevo
                    };

                    string jsonData = JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{URL_API}jugador/modificarmazo", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    } else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        return false;
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
    }
}