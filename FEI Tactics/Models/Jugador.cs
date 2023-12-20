using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics
{
    public class Jugador
    {
        private string Gamertag { get; set; }
        private string Contrasenia { get; set; }
        private int PartidasGanadas { get; set; }
        private int PartidasPerdidas { get; set; }
        private string Mazo { get; set; }
        private int IdFoto { get; set; }

        public Jugador(string gamertag, string contrasenia, int partidasGanadas, int partidasPerdidas, string mazo, int idFoto)
        {
            Gamertag = gamertag;
            this.Contrasenia = contrasenia;
            PartidasGanadas = partidasGanadas;
            PartidasPerdidas = partidasPerdidas;
            Mazo = mazo;
            IdFoto = idFoto;
        }

        public Jugador()
        {
        }
    }
}
