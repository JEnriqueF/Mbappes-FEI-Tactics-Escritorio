using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class Movimiento
    {
        public Movimiento(int iDEscenario, int iDCarta)
        {
            IDEscenario = iDEscenario;
            IDCarta = iDCarta;
        }

        public Movimiento() { }

        [JsonProperty("Escenario")]
        public int IDEscenario {  get; set; }
        [JsonProperty("Carta")]
        public int IDCarta { get; set; }
    }
}
