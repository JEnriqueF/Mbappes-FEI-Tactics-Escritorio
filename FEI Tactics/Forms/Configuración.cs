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
    public partial class Configuración : Form
    {
        private SoundPlayer player;

        public Configuración(SoundPlayer player)
        {
            this.player = player;
            InitializeComponent();

            // Cargar el estado del CheckBox desde la configuración de la aplicación
            cbSonido.Checked = Properties.Settings.Default.SonidoActivado;
        }

        private void controlSonido(object sender, EventArgs e)
        {
            if (cbSonido.Checked)
            {
                player.PlayLooping();
            } else
            {
                player.Stop();
            }

            // Guardar el estado actual del CheckBox en la configuración de la aplicación
            Properties.Settings.Default.SonidoActivado = cbSonido.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
