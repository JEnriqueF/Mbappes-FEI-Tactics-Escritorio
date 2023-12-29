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
    public partial class Partida : Form
    {
        JugadorResponse oponente;
        FotoPerfilResponse fotoPerfilOponente;
        List<Escenario> escenariosPartidaInfo;

        public Partida()
        {
            InitializeComponent();
            RecuperarEscenariosInfoAsync().ContinueWith(t =>
            {
                CargarImagenesEscenarios();
                buttonTerminarTurno.Enabled = true;
                buttonAbandonarPartida.Enabled = true;
                VerificarFotosObtenidas();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void buttonAbandonarPartida_Click(object sender, EventArgs e)
        {
            if (Mensaje.MostrarMensajeConfirmacion())
            {
                try
                {
                    string respuestaAbandonarPartida = await MatchMakingService.CancelarPartidaAsync(Jugador.Instancia.Gamertag);

                    if (respuestaAbandonarPartida.Equals("Partida cancelada correctamente"))
                    {
                        buttonAbandonarPartida.Enabled = false;
                        buttonTerminarTurno.Enabled = false;
                        this.Close();
                    }
                    else
                    {
                        Mensaje.MostrarMensaje("Jugador no encontrado en la partida. No se pudo cancelar.", "Jugador no encontrado", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
                }
            }
        }

        private void buttonTerminarTurno_Click(object sender, EventArgs e)
        {

        }

        private void VerificarFotosObtenidas()
        {
            if (escenariosPartidaInfo == null)
            {
                buttonTerminarTurno.Enabled = false;
                buttonAbandonarPartida.Enabled = true;
            }
        }

        public async Task ObtenerDatosOponente(string gamertag)
        {
            try
            {
                oponente = await JugadorService.RecuperarOponenteAsync(gamertag);

                if (oponente != null)
                {
                    lbOponente.Text = oponente.Gamertag;
                    RecuperarFotoPerfilOponenteAsync();
                }
                else
                {
                    Mensaje.MostrarMensaje("No se encontró al jugador oponente.", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private async void RecuperarFotoPerfilOponenteAsync()
        {
            try
            {
                List<FotoPerfil> fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();
                foreach (var fotoPerfil in fotosPerfil)
                {
                    if (fotoPerfil.IDFoto == oponente.IdFoto)
                    {
                        Image image = ConvertidorImagen.DeBase64AImagen(fotoPerfil.Foto);
                        fotoPerfilOponente = new FotoPerfilResponse(fotoPerfil.IDFoto, image);
                        pbFotoPerfilEnemigo.Image = fotoPerfilOponente.Foto;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        public async Task RecuperarEscenariosInfoAsync()
        {
            try
            {
                List<EscenarioResponse> escenarios = await EscenarioService.RecuperarEscenariosAsync();
                escenariosPartidaInfo = new List<Escenario>();

                foreach (var escenario in escenarios)
                {
                    Image image = ConvertidorImagen.DeBase64AImagen(escenario.Imagen);
                    escenariosPartidaInfo.Add(new Escenario(escenario.IDEscenario, image));
                }
            }
            catch (Exception ex)
            {
                buttonAbandonarPartida.Enabled = true;
                buttonTerminarTurno.Enabled = false;
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        public void CargarImagenesEscenarios()
        {
            if (escenariosPartidaInfo != null && escenariosPartidaInfo.Count > 0)
            {
                pbEscenario1.Image = escenariosPartidaInfo[0].Imagen;
                pbEscenario2.Image = escenariosPartidaInfo[1].Imagen;
                pbEscenario3.Image = escenariosPartidaInfo[2].Imagen;
            }
            else
            {
                Label labelSinContenido = new Label();
                labelSinContenido.Text = "Sin contenido.";
                labelSinContenido.ForeColor = Color.Red;
                labelSinContenido.BackColor = Color.Transparent;
                labelSinContenido.Location = new Point(pbEscenario1.Width / 2 - labelSinContenido.Width / 2, pbEscenario1.Height / 2 - labelSinContenido.Height / 2);
                pbEscenario1.Controls.Add(labelSinContenido);

                Label labelSinContenido2 = new Label();
                labelSinContenido2.Text = "Sin contenido.";
                labelSinContenido2.ForeColor = Color.Red;
                labelSinContenido2.BackColor = Color.Transparent;
                labelSinContenido2.Location = new Point(pbEscenario1.Width / 2 - labelSinContenido2.Width / 2, pbEscenario1.Height / 2 - labelSinContenido2.Height / 2);
                pbEscenario1.Controls.Add(labelSinContenido2);

                Label labelSinContenido3 = new Label();
                labelSinContenido3.Text = "Sin contenido.";
                labelSinContenido3.ForeColor = Color.Red;
                labelSinContenido3.BackColor = Color.Transparent;
                labelSinContenido3.Location = new Point(pbEscenario1.Width / 2 - labelSinContenido3.Width / 2, pbEscenario1.Height / 2 - labelSinContenido3.Height / 2);
                pbEscenario1.Controls.Add(labelSinContenido3);
            }
        }

    }
}
