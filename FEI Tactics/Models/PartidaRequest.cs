using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class PartidaRequest
    {
        public PartidaRequest(string gamertag, List<Movimiento> listaMovimientos)
        {
            Gamertag = gamertag;
            this.listaMovimientos = listaMovimientos;
        }

        public PartidaRequest() { }

        public string Gamertag {  get; set; }
        public List<Movimiento> listaMovimientos { get; set; }
    }
}
