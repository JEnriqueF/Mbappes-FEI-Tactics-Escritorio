﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Models
{
    public class FotoPerfilResponse
    {
        public int IDFoto { get; set; }
        public Image Foto { get; set; }

        public FotoPerfilResponse(int idFoto, Image foto)
        {
            this.IDFoto = idFoto;
            this.Foto = foto;
        }
    }
}
