using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class JugadorResponse
    {
        public string Gamertag { get; set; }
        public string Contrasenia { get; set; }
        public int PartidasGanadas { get; set; }
        public int PartidasPerdidas { get; set; }
        public string Mazo { get; set; }
        public int IdFoto { get; set; }

        public JugadorResponse(string gamertag, string contrasenia, int partidasGanadas, int partidasPerdidas, string mazo, int idFoto)
        {
            Gamertag = gamertag;
            this.Contrasenia = contrasenia;
            PartidasGanadas = partidasGanadas;
            PartidasPerdidas = partidasPerdidas;
            Mazo = mazo;
            IdFoto = idFoto;
        }

        public JugadorResponse()
        {
        }
    }
}
