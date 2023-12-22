using FEI_Tactics.Models;
using FEI_Tactics.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics.Forms
{
    public partial class RegistroCuenta : Form
    {
        private List<FotoPerfilInfo> fotosPerfilInfo;
        private int indiceImagenActual;
        public RegistroCuenta()
        {
            InitializeComponent();
            personalizarDiseñoForm();

            RecuperarFotosPerfilInfoAsync().ContinueWith(t =>
            {
                CargarImagenes();
                buttonCambiarFoto.Enabled = true;
                buttonRegistrar.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void personalizarDiseñoForm()
        {
            //Background
            BackColor = Color.Blue;
            Paint += (s, pe) =>
            {
                var rect = new Rectangle(0, 0, Width, Height);
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    rect, Color.Blue, Color.Orange, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal))
                {
                    pe.Graphics.FillRectangle(brush, rect);
                }
            };
            //Label
            this.labelTitulo.BackColor = Color.Transparent;
            this.labelTitulo.ForeColor = Color.White;
            this.labelGamertag.BackColor = Color.Transparent;
            this.labelGamertag.ForeColor = Color.White;
            this.labelPassword.BackColor = Color.Transparent;
            this.labelPassword.ForeColor = Color.White;
            this.labelSelecFotoPerfil.BackColor = Color.Transparent;
            this.labelSelecFotoPerfil.ForeColor = Color.White;
            //Button
            buttonRegresar.FlatStyle = FlatStyle.Flat;
            buttonRegresar.FlatAppearance.BorderSize = 0;
            buttonRegresar.BackColor = Color.Red;
            buttonRegresar.ForeColor = Color.White;
            buttonRegistrar.FlatStyle = FlatStyle.Flat;
            buttonRegistrar.FlatAppearance.BorderSize = 0;
            buttonRegistrar.BackColor = Color.Green;
            buttonRegistrar.ForeColor = Color.White;
            buttonCambiarFoto.FlatStyle = FlatStyle.Flat;
            buttonCambiarFoto.FlatAppearance.BorderSize = 0;
            buttonCambiarFoto.BackColor = Color.Green;
            buttonCambiarFoto.ForeColor = Color.White;
            //PictureBox
            pbFotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            pbFotoPerfil.BackColor = Color.Transparent;
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            if (!tbGamertag.Text.Trim().Equals("") || !tbPassword.Text.Trim().Equals(""))
            {
                if(Mensaje.MostrarMensajeConfirmacionRegresar())
                {
                    InicioSesion inicioSesion = new InicioSesion();
                    this.Close();
                    inicioSesion.Show();
                }
            }
            else
            {
                InicioSesion inicioSesion = new InicioSesion();
                this.Close();
                inicioSesion.Show();
            }
        }

        public async Task RecuperarFotosPerfilInfoAsync()
        {
            try
            {
                List<FotoPerfil> fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();
                fotosPerfilInfo = new List<FotoPerfilInfo>();

                foreach (var fotoPerfil in fotosPerfil)
                {
                    Image image = ConvertidorImagen.DeBase64AImagen(fotoPerfil.Foto);
                    fotosPerfilInfo.Add(new FotoPerfilInfo(fotoPerfil.IDFoto, image));
                }
            }catch(Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        public void CargarImagenes()
        {
            if (fotosPerfilInfo != null && fotosPerfilInfo.Count > 0)
            {
                pbFotoPerfil.Image = fotosPerfilInfo[0].Foto;
                indiceImagenActual = 0;
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

        private void buttonCambiarFoto_Click(object sender, EventArgs e)
        {
            indiceImagenActual++;
            if (indiceImagenActual >= fotosPerfilInfo.Count)
            {
                indiceImagenActual = 0;
            }
            pbFotoPerfil.Image = fotosPerfilInfo[indiceImagenActual].Foto;
        }

        private async void buttonRegistrar_Click(object sender, EventArgs e)
        {
            if (!(tbGamertag.Text.Trim().Equals("")) && !(tbPassword.Text.Trim().Equals("")))
            {
                if (Mensaje.MostrarMensajeConfirmacion())
                {
                    try
                    {
                        int idFoto = fotosPerfilInfo[indiceImagenActual].IDFoto;
                        Boolean registroExitoso = await JugadorService.RegistrarCuentaJugadorAsync(tbGamertag.Text.Trim(), tbPassword.Text.Trim(), idFoto);

                        if (registroExitoso)
                        {
                            Mensaje.MostrarMensaje("El jugador se registró correctamente", "Registro exitoso", MessageBoxIcon.Information);
                            tbGamertag.Text = "";
                            tbPassword.Text = "";
                        }
                        else
                        {
                            Mensaje.MostrarMensaje("El jugador no pudo ser registrado. Intente en otro momento.", "Registro incorrecto", MessageBoxIcon.Error);
                            tbGamertag.Text = "";
                            tbPassword.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        tbGamertag.Text = "";
                        tbPassword.Text = "";
                        Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                Mensaje.MostrarMensajeErrorCampos();
            }
        }

    }
}