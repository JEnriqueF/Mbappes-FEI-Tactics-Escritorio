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
            return MostrarMensajeConfirmacion("¿Está seguro de querer regresar? Si lo hace los datos ingresados se perderán.", "Regresar");
        }
        public static Boolean MostrarMensajeConfirmacion(string mensaje, string titulo)
        {
            DialogResult resultado = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (resultado == DialogResult.Yes);
        }

        public static void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }
    }
}
