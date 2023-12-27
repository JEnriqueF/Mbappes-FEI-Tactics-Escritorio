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
        int[] numerosMazo;
        List<Carta> cartas;
        List<PictureBox> pictureBoxesMazo = new List<PictureBox>();
        List<PictureBox> pictureBoxesDisponibles = new List<PictureBox>();
        List<Label> labelsCartasMazo = new List<Label>();
        List<Label> labelsCartasDisponibles = new List<Label>();

        public Mazo()
        {
            InitializeComponent();
        }

        private void MazoLoad(object sender, EventArgs e)
        {
            numerosMazo = Jugador.Instancia.Mazo.Split(',').Select(s => Convert.ToInt32(s.Trim())).ToArray();
            int indiceNumerosMazo = 0;
            
            cartas = Carta.Instancia;
            int indiceCarta = 0;
            int[] idCartas = new int[cartas.Count];
            
            int controladorDisponibles = 0;

            pictureBoxesMazo.Add(pbCarta1);
            pictureBoxesMazo.Add(pbCarta2);
            pictureBoxesMazo.Add(pbCarta3);
            pictureBoxesMazo.Add(pbCarta4);

            pictureBoxesDisponibles.Add(pbCarta5);
            pictureBoxesDisponibles.Add(pbCarta6);

            labelsCartasMazo.Add(lbCarta1);
            labelsCartasMazo.Add(lbCarta2);
            labelsCartasMazo.Add(lbCarta3);
            labelsCartasMazo.Add(lbCarta4);

            labelsCartasDisponibles.Add(lbCarta5);
            labelsCartasDisponibles.Add(lbCarta6);

            int controladorIdCarta = 0;
            foreach(Carta carta in cartas)
            {
                idCartas[controladorIdCarta] = carta.IDCarta;
                controladorIdCarta++;
            }
            
            foreach (int numero in idCartas)
            {
                if ( Jugador.Instancia.Mazo.Contains(numero.ToString()) && numero != 0 )
                {
                    indiceNumerosMazo = Array.IndexOf(numerosMazo, numero);
                    indiceCarta = cartas.FindIndex(x => x.IDCarta == numero);

                    pictureBoxesMazo[indiceNumerosMazo].Image = ConvertidorImagen.DeBase64AImagen(cartas[indiceCarta].Imagen);
                    labelsCartasMazo[indiceNumerosMazo].Text = numero.ToString();

                    numerosMazo[indiceNumerosMazo] = 0;
                } else if (numero != 0)
                {
                    indiceCarta = cartas.FindIndex(x => x.IDCarta == numero);

                    pictureBoxesDisponibles[controladorDisponibles].Image = ConvertidorImagen.DeBase64AImagen(cartas[indiceCarta].Imagen);
                    pictureBoxesDisponibles[controladorDisponibles].Enabled = false;

                    labelsCartasDisponibles[controladorDisponibles].Text = numero.ToString();
                    controladorDisponibles++;
                }
            }
        }

        private void activarAccionDisponibles()
        {
            for (int i = 0; i < pictureBoxesDisponibles.Count; i++)
            {
                pictureBoxesDisponibles[i].Enabled = true;
            }
        }

        private void desactivarAccionDisponibles()
        {
            for (int i = 0; i < pictureBoxesDisponibles.Count; i++)
            {
                pictureBoxesDisponibles[i].Enabled = false;
            }
        }

        private void activarBotones()
        {
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }

        private void desactivarBotones()
        {
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void mazoVisible() {
            for (int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                pictureBoxesMazo[i].Visible = true;
            }
        }

        private void clicEditarMazo(object sender, EventArgs e)
        {

        }

        private void clicCancelarCambioMazo(object sender, EventArgs e)
        {
            mazoVisible();
            desactivarBotones();
            desactivarAccionDisponibles();
        }

        private void seleccionarCarta1(object sender, EventArgs e)
        {
            pbCarta2.Visible = false;
            pbCarta3.Visible = false;
            pbCarta4.Visible = false;

            activarBotones();
            activarAccionDisponibles();
        }

        private void seleccionarCarta2(object sender, EventArgs e)
        {
            pbCarta1.Visible = false;
            pbCarta3.Visible = false;
            pbCarta4.Visible = false;

            activarBotones();
            activarAccionDisponibles();
        }

        private void seleccionarCarta3(object sender, EventArgs e)
        {
            pbCarta1.Visible = false;
            pbCarta2.Visible = false;
            pbCarta4.Visible = false;

            activarBotones();
            activarAccionDisponibles();
        }

        private void seleccionarCarta4(object sender, EventArgs e)
        {
            pbCarta1.Visible = false;
            pbCarta2.Visible = false;
            pbCarta3.Visible = false;

            activarBotones();
            activarAccionDisponibles();
        }

        private void cambiarDisponible1(object sender, EventArgs e)
        {
            for(int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                if (pictureBoxesMazo[i].Visible)
                {
                    Image imgTemp = pbCarta5.Image;
                    pbCarta5.Image = pictureBoxesMazo[i].Image;
                    pictureBoxesMazo[i].Image = imgTemp;

                    string idCartaTempo = lbCarta5.Text;
                    lbCarta5.Text = labelsCartasMazo[i].Text;
                    labelsCartasMazo[i].Text = idCartaTempo;

                    mazoVisible();
                    desactivarBotones();
                }
            }
        }

        private void cambiarDisponible2(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                if (pictureBoxesMazo[i].Visible)
                {
                    Image imgTemp = pbCarta6.Image;
                    pbCarta6.Image = pictureBoxesMazo[i].Image;
                    pictureBoxesMazo[i].Image = imgTemp;

                    string idCartaTempo = lbCarta6.Text;
                    lbCarta6.Text = labelsCartasMazo[i].Text;
                    labelsCartasMazo[i].Text = idCartaTempo;

                    mazoVisible();
                    desactivarBotones();
                }
            }
        }
    }
}
