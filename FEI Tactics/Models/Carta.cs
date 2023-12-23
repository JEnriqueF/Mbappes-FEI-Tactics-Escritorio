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

        public static void Inicializar(int idCarta, int costo, int poder, string imagen)
        {
            if (lazyInstancia.IsValueCreated)
            {
                return;
            }

            var nuevaCarta = new Carta
            {
                IDCarta = idCarta,
                Costo = costo,
                Poder = poder,
                Imagen = imagen
            };

            Instancia.Add(nuevaCarta);
        }

        public int IDCarta { get; set; }
        public int Costo { get; set; }
        public int Poder { get; set; }
        public string Imagen { get; set; }
    }
}
