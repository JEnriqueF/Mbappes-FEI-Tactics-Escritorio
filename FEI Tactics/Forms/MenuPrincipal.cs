using FEI_Tactics.Forms;
using FEI_Tactics.Models;
using FEI_Tactics.Services;
using FEI_Tactics.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics
{
    public partial class MenuPrincipal : Form
    {
        private bool cancelarBusqueda = false;

        FotoPerfilResponse fotoActual;
        public MenuPrincipal()
        {
            InitializeComponent();
            RecuperarFotoPerfilActualAsync().ContinueWith(t =>
            {
                cargarDatosPerfilActual();
                buttonBuscarPartida.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void cargarDatosPerfilActual()
        {
            lbGamertag.Text = "Bienvenido(a)" + " " + Jugador.Instancia.Gamertag;
            pbFotoPerfil.Image = fotoActual.Foto;
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            metodoCancelarBusqueda();
        }

        private async void buttonBuscarPartida_Click(object sender, EventArgs e)
        {
            try
            {
                MatchMakingResponse respuestaSolicitudPartida;
                buttonBuscarPartida.Visible = false;
                buttonCancelar.Visible = true;
                int cicloBusqueda = 0;

                do
                {
                    if (cicloBusqueda == 5)
                    {
                        metodoCancelarBusqueda();
                    }
                    if (cancelarBusqueda)
                    {
                        buttonBuscarPartida.Visible = true;
                        cancelarBusqueda = false;
                        return;
                    }
                    respuestaSolicitudPartida = await MatchMakingService.SolicitarPartidaAsync(Jugador.Instancia.Gamertag);
                    cicloBusqueda++;
                } while (respuestaSolicitudPartida.Respuesta != null && (respuestaSolicitudPartida.Respuesta.Equals("Ya se solicitó la partida") || respuestaSolicitudPartida.Respuesta.Equals("Solicitud Guardada") || respuestaSolicitudPartida.Respuesta.Equals("Partida Creada")));

                if (!respuestaSolicitudPartida.Gamertag.Equals(Jugador.Instancia.Gamertag))
                {
                    buttonCancelar.Visible = false;
                    buttonBuscarPartida.Visible = true;
                    Partida partida = new Partida(respuestaSolicitudPartida.Gamertag);
                    partida.Show();
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private async void metodoCancelarBusqueda()
        {
            try
            {
                string respuestaCancelarBusqueda = await MatchMakingService.CancelarBusquedaAsync(Jugador.Instancia.Gamertag);

                if (respuestaCancelarBusqueda.Equals("Jugador eliminado correctamente"))
                {
                    cancelarBusqueda = true;
                    buttonCancelar.Visible = false;
                }
            } catch(Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }
    }
}
