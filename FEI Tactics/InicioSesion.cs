using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void formClosing(object sender, FormClosingEventArgs fcea)
        {
            tbGamertag.Text = "";
            tbContrasenia.Text = "";

            this.Show();
        }

        private async void clicBtnIniciarSesion(object sender, EventArgs e)
        {
            if(tbGamertag.Text.Trim().Equals("") || tbContrasenia.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ambos campos son necesarios para acceder al juego", "Error en campos");
                return;
            }

            try
            {
                APIClient apiClient = new APIClient();
                string result = await apiClient.autenticarInicioSesionAsync(tbGamertag.Text, tbContrasenia.Text);

                Menu menu = new Menu();
                menu.Show();
                this.Hide();
                menu.FormClosing += formClosing;
            } catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de autenticación: Credenciales incorrectas");
            }
        }
    }
}
