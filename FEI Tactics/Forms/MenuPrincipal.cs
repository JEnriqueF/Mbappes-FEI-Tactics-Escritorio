using FEI_Tactics.Models;
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
    }
}
