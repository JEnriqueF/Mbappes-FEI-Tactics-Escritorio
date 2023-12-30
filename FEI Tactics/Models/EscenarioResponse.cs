using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class EscenarioResponse
    {
        public EscenarioResponse(int iDEscenario, string imagen)
        {
            IDEscenario = iDEscenario;
            Imagen = imagen;
        }
        public EscenarioResponse() { }

        public int IDEscenario { get; set; }
        public string Imagen { get; set; }
    }
}
