using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics
{
    public sealed class Jugador
    {
        private static readonly Lazy<Jugador> lazyInstancia = new Lazy<Jugador>(() => new Jugador(), isThreadSafe: true);
        private Jugador() { }
        public static Jugador Instancia => lazyInstancia.Value;

        public static void Inicializar(string gamertag, string contrasenia, int partidasGanadas, int partidasPerdidas, string mazo, int idFoto)
        {
            if (lazyInstancia.IsValueCreated)
            {
                return;
            }

            Instancia.Gamertag = gamertag;
            Instancia.Contrasenia = contrasenia;
            Instancia.PartidasGanadas = partidasGanadas;
            Instancia.PartidasPerdidas = partidasPerdidas;
            Instancia.Mazo = mazo;
            Instancia.IdFoto = idFoto;
        }

        public static void ActualizarMazo(string mazoActualizado)
        {
            Instancia.Mazo = mazoActualizado;
        }

        public static void ActualizarFotoPerfil(int nuevaFotoPerfil)
        {
            Instancia.IdFoto = nuevaFotoPerfil;
        }

        public string Gamertag { get; set; }
        public string Contrasenia { get; set; }
        public int PartidasGanadas { get; set; }
        public int PartidasPerdidas { get; set; }
        public string Mazo { get; set; }
        public int IdFoto { get; set; }
    }
}
