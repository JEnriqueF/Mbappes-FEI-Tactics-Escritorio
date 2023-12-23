using FEI_Tactics.Models;
using FEI_Tactics.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics
{
    public partial class Mazo : Form
    {
        public Mazo()
        {
            InitializeComponent();
        }

        private void MazoLoad(object sender, EventArgs e)
        {
            MessageBox.Show(Jugador.Instancia.Mazo);

            pbCarta1.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[0].Imagen);
            pbCarta2.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[1].Imagen);
            pbCarta3.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[2].Imagen);
            pbCarta4.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[3].Imagen);
            pbCarta5.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[4].Imagen);
            pbCarta6.Image = ConvertidorImagen.DeBase64AImagen(Carta.Instancia[5].Imagen);
        }
    }
}
