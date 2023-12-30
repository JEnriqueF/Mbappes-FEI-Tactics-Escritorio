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
        string gamertagOponente;
        List<Escenario> escenarios;
        List<PictureBox> pictureBoxesMazo = new List<PictureBox>();

        public Partida(string gamertagOponente)
        {
            InitializeComponent();
            this.gamertagOponente = gamertagOponente;
            EstablecerOrdenEjecucion();
        }

        private void EstablecerOrdenEjecucion()
        {
            ObtenerDatosOponente(gamertagOponente)
            .ContinueWith(t =>
            {
                return RecuperarEscenariosInfoAsync();
            }, TaskScheduler.FromCurrentSynchronizationContext())
            .Unwrap()
            .ContinueWith(t =>
            {
                CargarImagenesEscenarios();
                buttonTerminarTurno.Enabled = true;
                buttonAbandonarPartida.Enabled = true;
                VerificarFotosObtenidas();
                CargarMazo();
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
                        return;
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
                escenarios = await EscenarioService.RecuperarEscenariosAsync();
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
            if (escenarios != null && escenarios.Count > 0)
            {
                pbEscenario1.Image = ConvertidorImagen.DeBase64AImagen(escenarios[0].Imagen);
                pbEscenario2.Image = ConvertidorImagen.DeBase64AImagen(escenarios[1].Imagen);
                pbEscenario3.Image = ConvertidorImagen.DeBase64AImagen(escenarios[2].Imagen);
            }
            else
            {
                Mensaje.MostrarMensaje("No existen escenarios.", "Error al recuperar los escenarios", MessageBoxIcon.Error);
            }
        }

        public void CargarMazo()
        {
            int[] numerosMazo = Jugador.Instancia.Mazo.Split(',').Select(s => Convert.ToInt32(s.Trim())).ToArray();
            int indiceNumerosMazo = 0;
            List<Carta> cartas = Carta.Instancia;
            int indiceCarta = 0;

            int[] idCartas = new int[cartas.Count];

            pictureBoxesMazo.Add(pbMiCarta1);
            pictureBoxesMazo.Add(pbMiCarta2);
            pictureBoxesMazo.Add(pbMiCarta3);
            pictureBoxesMazo.Add(pbMiCarta4);

            int controladorIdCarta = 0;
            foreach (Carta carta in cartas)
            {
                idCartas[controladorIdCarta] = carta.IDCarta;
                controladorIdCarta++;
            }

            foreach(int numero in idCartas)
            {
                if (Jugador.Instancia.Mazo.Contains(numero.ToString()) && numero != 0)
                {
                    indiceNumerosMazo = Array.IndexOf(numerosMazo, numero);
                    indiceCarta = cartas.FindIndex(x => x.IDCarta == numero);

                    pictureBoxesMazo[indiceNumerosMazo].Image = ConvertidorImagen.DeBase64AImagen(cartas[indiceCarta].Imagen);

                    numerosMazo[indiceNumerosMazo] = 0;
                }
            }
        }
    }
}
