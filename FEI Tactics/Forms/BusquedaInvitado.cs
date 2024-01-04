using FEI_Tactics.Models;
using FEI_Tactics.Services;
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

namespace FEI_Tactics.Forms
{
    public partial class BusquedaInvitado : Form
    {
        private Random random = new Random();
        string Gamertag;
        private bool cancelarBusquedaInvitado = false;

        public BusquedaInvitado()
        {
            InitializeComponent();
        }

        private void BusquedaInvitado_Load(object sender, EventArgs e)
        {
            int numeroAleatorio = random.Next(1, 1000);
            Gamertag = "guest" + numeroAleatorio.ToString();
        }

        private async void btnBuscarPartida(object sender, EventArgs e)
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
                    if (cancelarBusquedaInvitado)
                    {
                        buttonBuscarPartida.Visible = true;
                        cancelarBusquedaInvitado = false;
                        return;
                    }
                    respuestaSolicitudPartida = await MatchMakingService.SolicitarPartidaAsync(Gamertag);
                    cicloBusqueda++;
                } while (respuestaSolicitudPartida.Respuesta != null && (respuestaSolicitudPartida.Respuesta.Equals("Ya se solicitó la partida") || 
                    respuestaSolicitudPartida.Respuesta.Equals("Solicitud Guardada") || respuestaSolicitudPartida.Respuesta.Equals("Partida Creada")));

                if ( !respuestaSolicitudPartida.Gamertag.Equals(Gamertag) )
                {
                    buttonCancelar.Visible = false;
                    buttonBuscarPartida.Visible = true;

                    Partida partida = new Partida(respuestaSolicitudPartida.Gamertag, Gamertag);
                    DialogResult result = partida.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            } catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private void btnCancelarBusqueda(object sender, EventArgs e)
        {
            metodoCancelarBusqueda();
        }

        private async void metodoCancelarBusqueda()
        {
            try
            {
                string respuestaCancelarBusqueda = await MatchMakingService.CancelarBusquedaAsync(Gamertag);

                if (respuestaCancelarBusqueda.Equals("Jugador eliminado correctamente"))
                {
                    cancelarBusquedaInvitado = true;
                    buttonCancelar.Visible = false;
                }
            } catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }
    }
}
