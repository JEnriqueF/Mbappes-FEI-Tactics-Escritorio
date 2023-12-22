using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_Tactics.Utilities
{
    public class ConvertidorImagen
    {
        public static Image DeBase64AImagen(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    return Image.FromStream(ms, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fallo al obtener las imágenes.", ex);
            }
        }
    }
}
