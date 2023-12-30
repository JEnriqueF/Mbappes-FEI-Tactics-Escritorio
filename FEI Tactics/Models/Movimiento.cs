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

        public int IDEscenario {  get; set; }
        public int IDCarta { get; set; }
    }
}
