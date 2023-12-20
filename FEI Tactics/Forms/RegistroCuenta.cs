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
        private List<FotoPerfil> fotosPerfil;
        public RegistroCuenta()
        {
            InitializeComponent();
            configurarDataGridView();
            personalizarDiseñoForm();
            cargarFotosPerfil();
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
            //verificarImagenCargada();
        }

        private void configurarDataGridView()
        {
            dgFotosPerfil.AutoGenerateColumns = false;
            //dgFotosPerfil.AllowUserToAddRows = false;
            dgFotosPerfil.RowTemplate.Height = 100;

            // Columna para la imagen
            DataGridViewImageColumn imagenColumn = new DataGridViewImageColumn();
            imagenColumn.Width = 344;
            imagenColumn.DataPropertyName = "Foto";
            imagenColumn.Name = "Imagen";
            imagenColumn.HeaderText = "Imagen";
            imagenColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgFotosPerfil.Columns.Add(imagenColumn);
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

        private async void cargarFotosPerfil()
        {
            try
            {
                fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();

                dgFotosPerfil.Rows.Clear();

                if (fotosPerfil != null && fotosPerfil.Count > 0)
                {
                    foreach (var fotoPerfil in fotosPerfil)
                    {
                        try
                        {
                            if (IsValidBase64String(fotoPerfil.Foto))
                            {
                                byte[] imagenBytes = Convert.FromBase64String(fotoPerfil.Foto);

                                using (MemoryStream ms = new MemoryStream(imagenBytes))
                                {
                                    Image imagen = Image.FromStream(ms);

                                    DataGridViewRow row = (DataGridViewRow)dgFotosPerfil.Rows[0].Clone();
                                    row.Cells[0].Value = fotoPerfil.IDFoto;
                                    row.Cells[1].Value = imagen;
                                    dgFotosPerfil.Rows.Add(row);
                                }
                            }
                            else
                            {
                                Mensaje.MostrarMensaje($"La cadena base64 para ID {fotoPerfil.IDFoto} no es válida.", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            Mensaje.MostrarMensaje($"Error al procesar la imagen para ID {fotoPerfil.IDFoto}: {ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    Label labelSinContenido = new Label();
                    labelSinContenido.Text = "Sin contenido.";
                    labelSinContenido.ForeColor = Color.Red;
                    labelSinContenido.Location = new Point(dgFotosPerfil.Width / 2 - labelSinContenido.Width / 2, dgFotosPerfil.Height / 2 - labelSinContenido.Height / 2);
                    dgFotosPerfil.Controls.Add(labelSinContenido);
                }
            }
            catch (Exception except)
            {
                Mensaje.MostrarMensaje($"{except.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private bool IsValidBase64String(string base64String)
        {
            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}