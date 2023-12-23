using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class CartaResponse
    {
        public int IDCarta { get; set; }
        public int Costo { get; set; }
        public int Poder { get; set; }
        public string Imagen { get; set; }

        public CartaResponse(int iDCarta, int costo, int poder, string imagen)
        {
            IDCarta = iDCarta;
            Costo = costo;
            Poder = poder;
            Imagen = imagen;
        }

        public CartaResponse() { }
    }
}
