using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public sealed class Carta
    {
        private static readonly Lazy<List<Carta>> lazyInstancia = new Lazy<List<Carta>>(() => new List<Carta>(), isThreadSafe: true);
        private Carta() { }
        public static List<Carta> Instancia => lazyInstancia.Value;
        private static int controladorCount = 0;

        public static void Inicializar(int idCarta, int costo, int poder, string imagen)
        {
            var nuevaCarta = new Carta
            {
                IDCarta = idCarta,
                Costo = costo,
                Poder = poder,
                Imagen = imagen
            };

            Instancia.Add(nuevaCarta);
            controladorCount++;
        }

        [JsonProperty("IDCarta")]
        public int IDCarta { get; set; }
        [JsonProperty("Costo")]
        public int Costo { get; set; }
        [JsonProperty("Poder")]
        public int Poder { get; set; }
        [JsonProperty("Imagen")]
        public string Imagen { get; set; }
    }
}
