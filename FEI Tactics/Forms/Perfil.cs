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
    public partial class Perfil : Form
    {
        FotoPerfilResponse fotoActual;
        List<FotoPerfilResponse> fotosPerfilDisponibles;
        List<PictureBox> pictureBoxes;
        PictureBox pictureBoxSeleccionado;

        public Perfil()
        {
            InitializeComponent();
            tbPartidasGanadas.BackColor = Color.White;
            establecerOrdenDeEjecucion();
        }

        private void establecerOrdenDeEjecucion()
        {
            RecuperarFotoPerfilActualAsync()
            .ContinueWith(t =>
            {
                cargarPerfilActual();
                return RecuperarFotosPerfilDisponiblesAsync();
            }, TaskScheduler.FromCurrentSynchronizationContext())
            .Unwrap()
            .ContinueWith(t =>
            {
                cargarFotosPerfilDisponibles();
                buttonGuardarCambios.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void pbFotoDisponible1_Click(object sender, EventArgs e)
        {
            DesseleccionarPictureBoxs();
            pbFotoDisponible1.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSeleccionado = pbFotoDisponible1;
        }

        private void pbFotoDisponible2_Click(object sender, EventArgs e)
        {
            DesseleccionarPictureBoxs();
            pbFotoDisponible2.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSeleccionado = pbFotoDisponible2;
        }

        private void pbFotoDisponible3_Click(object sender, EventArgs e)
        {
            DesseleccionarPictureBoxs();
            pbFotoDisponible3.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSeleccionado = pbFotoDisponible3;
        }

        private void pbFotoDisponible4_Click(object sender, EventArgs e)
        {
            DesseleccionarPictureBoxs();
            pbFotoDisponible4.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSeleccionado = pbFotoDisponible4;
        }

        private void pbFotoDisponible5_Click(object sender, EventArgs e)
        {
            DesseleccionarPictureBoxs();
            pbFotoDisponible5.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSeleccionado = pbFotoDisponible5;
        }

        private void DesseleccionarPictureBoxs()
        {
            pbFotoDisponible1.BorderStyle = BorderStyle.None;
            pbFotoDisponible2.BorderStyle = BorderStyle.None;
            pbFotoDisponible3.BorderStyle = BorderStyle.None;
            pbFotoDisponible4.BorderStyle = BorderStyle.None;
            pbFotoDisponible5.BorderStyle = BorderStyle.None;
            pictureBoxSeleccionado = null;
        }

        private async void buttonGuardarCambios_Click(object sender, EventArgs e)
        {
            if (pictureBoxSeleccionado != null)
            {
                if (Mensaje.MostrarMensajeConfirmacion())
                {
                    try
                    {
                        Boolean modificacionExitosa = await JugadorService.ModificarImagenPerfilAsync(Jugador.Instancia.Gamertag, (int)pictureBoxSeleccionado.Tag);


                        if (modificacionExitosa)
                        {
                            buttonGuardarCambios.Enabled = false;
                            Mensaje.MostrarMensaje("La foto de perfil del jugador se modificó correctamente", "Modificación exitosa", MessageBoxIcon.Information);
                            Jugador.ActualizarFotoPerfil((int)pictureBoxSeleccionado.Tag);
                            DesseleccionarPictureBoxs();
                            establecerOrdenDeEjecucion();
                        }
                        else
                        {
                            Mensaje.MostrarMensaje("La foto de perfil del jugador no pudo ser modificada correctamente. Intente en otro momento.", "Modificación incorrecta", MessageBoxIcon.Error);
                            DesseleccionarPictureBoxs();
                        }
                    }
                    catch (Exception ex)
                    {
                        DesseleccionarPictureBoxs();
                        Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DesseleccionarPictureBoxs();
                }
            }
            else
            {
                Mensaje.MostrarMensajeErrorCampos();
            }
        }

        private async Task RecuperarFotoPerfilActualAsync()
        {
            try
            {
                List<FotoPerfil> fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();
                foreach (var fotoPerfil in fotosPerfil)
                {
                    if (fotoPerfil.IDFoto == Jugador.Instancia.IdFoto)
                    {
                        Image image = ConvertidorImagen.DeBase64AImagen(fotoPerfil.Foto);
                        fotoActual = new FotoPerfilResponse(fotoPerfil.IDFoto, image);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private async Task RecuperarFotosPerfilDisponiblesAsync()
        {
            try
            {
                List<FotoPerfil> fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();
                fotosPerfilDisponibles = new List<FotoPerfilResponse>();

                foreach (var fotoPerfil in fotosPerfil)
                {
                    if (fotoPerfil.IDFoto != fotoActual.IDFoto)
                    {
                        Image image = ConvertidorImagen.DeBase64AImagen(fotoPerfil.Foto);
                        fotosPerfilDisponibles.Add(new FotoPerfilResponse(fotoPerfil.IDFoto, image));
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private void cargarPerfilActual()
        {
            lbGamertag.Text = Jugador.Instancia.Gamertag;
            tbPartidasGanadas.Text = Jugador.Instancia.PartidasGanadas.ToString();
            if (fotoActual != null)
            {
                pbFotoPerfil.Image = fotoActual.Foto;
            }
            else
            {
                Label labelSinContenido = new Label();
                labelSinContenido.Text = "Sin contenido.";
                labelSinContenido.ForeColor = Color.Red;
                labelSinContenido.BackColor = Color.Transparent;
                labelSinContenido.Location = new Point(pbFotoPerfil.Width / 2 - labelSinContenido.Width / 2, pbFotoPerfil.Height / 2 - labelSinContenido.Height / 2);
                pbFotoPerfil.Controls.Add(labelSinContenido);
            }
        }

        private void cargarFotosPerfilDisponibles()
        {
            pictureBoxes = new List<PictureBox> { pbFotoDisponible1, pbFotoDisponible2, pbFotoDisponible3, pbFotoDisponible4, pbFotoDisponible5 };
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Image = null;
                pictureBox.Controls.Clear();
            }

            if (fotosPerfilDisponibles != null && fotosPerfilDisponibles.Count > 0)
            {
                for (int i = 0; i < Math.Min(fotosPerfilDisponibles.Count, pictureBoxes.Count); i++)
                {
                    pictureBoxes[i].Image = fotosPerfilDisponibles[i].Foto;
                    pictureBoxes[i].Tag = fotosPerfilDisponibles[i].IDFoto;
                }
            }
            else
            {
                mostrarEtiquetaSinContenido();
            }
        }

        private void mostrarEtiquetaSinContenido()
        {
            foreach (var pictureBox in pictureBoxes)
            {
                Label labelSinContenido = new Label();
                labelSinContenido.Text = "Sin contenido.";
                labelSinContenido.ForeColor = Color.Red;
                labelSinContenido.BackColor = Color.Transparent;
                labelSinContenido.Location = new Point(pictureBox.Width / 2 - labelSinContenido.Width / 2, pictureBox.Height / 2 - labelSinContenido.Height / 2);
                pictureBox.Controls.Add(labelSinContenido);
            }
        }
    }
}
