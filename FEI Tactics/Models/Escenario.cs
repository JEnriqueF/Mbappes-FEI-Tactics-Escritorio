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
        public int IDEscenario { get; set; }
        public Image Imagen { get; set; }

        public Escenario() { }

        public Escenario(int IDEscenario, Image Imagen)
        {
            this.IDEscenario = IDEscenario;
            this.Imagen = Imagen;
        }
    }
}
