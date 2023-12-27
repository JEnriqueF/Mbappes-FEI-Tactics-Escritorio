using FEI_Tactics.Models;
using FEI_Tactics.Services;
using FEI_Tactics.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            personalizarDiseñoForm();
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
            //Labels
            this.labelGamertag.BackColor = Color.Transparent;
            this.labelPassword.BackColor = Color.Transparent;
            this.labelTitulo.BackColor = Color.Transparent;
            this.labelGamertag.ForeColor = Color.White;
            this.labelPassword.ForeColor = Color.White;
            this.labelTitulo.ForeColor = Color.White;
            //Buttons
            buttonInicioSesion.FlatStyle = FlatStyle.Flat;
            buttonInicioSesion.FlatAppearance.BorderSize = 0;
            buttonInicioSesion.BackColor = Color.Green;
            buttonInicioSesion.ForeColor = Color.White;
            buttonRegistrarse.FlatStyle = FlatStyle.Flat;
            buttonRegistrarse.FlatAppearance.BorderSize = 0;
            buttonRegistrarse.BackColor = Color.Red;
            buttonRegistrarse.ForeColor = Color.White;
            buttonInvitado.FlatStyle = FlatStyle.Flat;
            buttonInvitado.FlatAppearance.BorderSize = 0;
            buttonInvitado.BackColor = Color.Gold;
            buttonInvitado.ForeColor = Color.White;
        }

        private void formClosing(object sender, FormClosingEventArgs fcea)
        {
            tbGamertag.Text = "";
            tbContrasenia.Text = "";
            this.Close();
        }

        private async void clicBtnIniciarSesion(object sender, EventArgs e)
        {
            if(tbGamertag.Text.Trim().Equals("") || tbContrasenia.Text.Trim().Equals(""))
            {
                Mensaje.MostrarMensajeErrorCampos();
                return;
            }
            try
            {
                JugadorResponse sesionJugadorActual = await JugadorService.AutenticarInicioSesionAsync(tbGamertag.Text, tbContrasenia.Text);
                List<Carta> mazoJugador = await CartasService.RecuperarMazoAsync();

                if (sesionJugadorActual != null)
                {
                    if(mazoJugador == null)
                    {
                        Mensaje.MostrarMensaje("No existen cartas registradas en el juego.", "Error", MessageBoxIcon.Error);
                        return;
                    } else
                    {
                        foreach(Carta ma in mazoJugador){
                            Carta.Inicializar(
                                ma.IDCarta,
                                ma.Costo,
                                ma.Poder,
                                ma.Imagen,
                                mazoJugador.Count
                            );
                        }
                    }

                    Jugador.Inicializar(
                        sesionJugadorActual.Gamertag,
                        sesionJugadorActual.Contrasenia,
                        sesionJugadorActual.PartidasGanadas,
                        sesionJugadorActual.PartidasPerdidas,
                        sesionJugadorActual.Mazo,
                        sesionJugadorActual.IdFoto
                    );

                    Menu menu = new Menu();
                    menu.Show();
                    this.Hide();
                    menu.FormClosing += formClosing;
                }
                else
                {
                    Mensaje.MostrarMensaje("Las credenciales ingresadas no coinciden con ninguna cuenta.", "Error de autenticación", MessageBoxIcon.Error);
                }
            } catch (Exception except)
            {
                Mensaje.MostrarMensaje($"{except.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private void buttonRegistrarse_Click(object sender, EventArgs e)
        {
            Forms.RegistroCuenta registroCuenta = new Forms.RegistroCuenta();
            this.Hide();
            registroCuenta.Show();
        }
    }
}
