using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class PartidaResponse
    {
        public PartidaResponse(string respuesta, List<Movimiento> listaMovimientos)
        {
            Respuesta = respuesta;
            this.listaMovimientos = listaMovimientos;
        }

        public PartidaResponse() { }

        public string Respuesta {  get; set; }
        [JsonProperty("Movimientos")]
        public List<Movimiento> listaMovimientos { get; set;}
    }
}
