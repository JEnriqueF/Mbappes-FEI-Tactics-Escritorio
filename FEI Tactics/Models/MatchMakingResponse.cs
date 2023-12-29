using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class MatchMakingResponse
    {
        public string Respuesta { get; set; }
        public string Gamertag { get; set; }

        public MatchMakingResponse(string respuesta, string gamertag)
        {
            Respuesta = respuesta;
            Gamertag = gamertag;
        }
    }
}
