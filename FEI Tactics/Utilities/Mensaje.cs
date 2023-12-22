using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics.Utilities
{
    public static class Mensaje
    {
        public static void MostrarMensajeErrorCampos()
        {
            MostrarMensaje("Todos los campos son necesarios.", "Campos faltantes", MessageBoxIcon.Error);
        }
        public static Boolean MostrarMensajeConfirmacionRegresar()
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de querer regresar? Si lo hace los datos ingresados se perderán.", "Regresar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (resultado == DialogResult.Yes);
        }
        public static Boolean MostrarMensajeConfirmacion()
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de querer continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (resultado == DialogResult.Yes);
        }

        public static void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }
    }
}
