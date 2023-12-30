using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class Escenario
    {
        [JsonProperty("IDEscenario")]
        public int IDEscenario { get; set; }
        [JsonProperty("Imagen")]
        public string Imagen { get; set; }

        public Escenario() { }

        public Escenario(int IDEscenario, string Imagen)
        {
            this.IDEscenario = IDEscenario;
            this.Imagen = Imagen;
        }
    }
}
