﻿using FontAwesome.Sharp;
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
    public partial class Menu : Form
    {
        private IconMenuItem menuActivo = null;
        private Form formularioActivo = null;

        public Menu()
        {
            InitializeComponent();
        }

        private void abrirForm(IconMenuItem menu, Form form)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }
            formularioActivo = form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.BackColor = Color.White;
            contenedor.Controls.Add(form);
            form.Show();
        }

        private void menuPrincipal(object sender, EventArgs e)
        {
            abrirForm((IconMenuItem)sender, new MenuPrincipal());
        }

        private void mazo(object sender, EventArgs e)
        {
            abrirForm((IconMenuItem)sender, new Mazo());
        }

        private void perfil(object sender, EventArgs e)
        {
            abrirForm((IconMenuItem)sender, new Perfil());
        }

        private void configuracion(object sender, EventArgs e)
        {
            abrirForm((IconMenuItem)sender, new Configuración());
        }
    }
}
