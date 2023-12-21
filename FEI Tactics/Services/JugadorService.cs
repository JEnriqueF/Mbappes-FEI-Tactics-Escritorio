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
        private const string URL_API = "https://18n4qv9q-3000.usw3.devtunnels.ms/";

        public static async Task<Jugador> AutenticarInicioSesionAsync(string gamertag, string password)
        {
            Jugador jugadorActual = new Jugador();
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
                        jugadorActual = JsonConvert.DeserializeObject<Jugador>(responseJson);
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

        public static async Task<Boolean> RegistrarCuentaJugadorAsync(string gamertag, string password, int partidasGanadas, int partidasPerdidas, string[] mazo, int fotoPerfil)
        {
            Boolean registroExitoso = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new
                    {
                        Gamertag = gamertag,
                        partidasGanadas = partidasGanadas,
                        PartidasPerdidas = partidasPerdidas,
                        Mazo = mazo,
                        IDFoto = fotoPerfil,
                        contrasenia = password
                    };
                    string jsonData = JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{URL_API}jugador/registrarjugador", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        registroExitoso = true;
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
            return registroExitoso;
        }

        public static async Task<List<FotoPerfil>> RecuperarFotosPerfilAsync()
        {
            List<FotoPerfil> fotosPerfil = new List<FotoPerfil>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{URL_API}jugador/recuperarImagenesPerfil");

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


    }
}