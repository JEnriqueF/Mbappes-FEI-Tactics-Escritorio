using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class FotoPerfil
    {
        [JsonProperty("IDFoto")]
        public int IDFoto { get; set; }
        [JsonProperty("Foto")]
        public string Foto { get; set; }

        public FotoPerfil() { }

        public FotoPerfil(int idFotoPerfil, string foto)
        {
            this.IDFoto = idFotoPerfil;
            this.Foto = foto;
        }
    }
}
