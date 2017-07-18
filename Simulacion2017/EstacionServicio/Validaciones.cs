using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EstacionServicio
{
    public class Validaciones
    {

        public static bool validarNumero(TextBox txt)
        {
            bool resul = true;

            try
            {
                Convert.ToDouble(txt.Text);
            }
            catch (FormatException ex)
            {
                resul = false;
            }
            return resul;
        }

        public static bool estaVacio(TextBox txt)
        {
            bool resul = true;

            if (txt.Text.Equals(String.Empty))
            {
                resul = false;
            }

            return resul;

        }
        public static bool validarDatosFormato(GroupBox grupo)
        {
            bool result = true;
            foreach (Control cont in grupo.Controls)
            {
                if (cont is TextBox)
                {
                    if (!validarNumero((TextBox)cont))
                    {

                        return result = false;
                    }
                }
            }

            return result;
        }
        public static bool validarControlVacio(GroupBox grupo)
        {
            bool result = true;

            foreach (Control cont in grupo.Controls)
            {
                if (cont is TextBox)
                {
                    if (!estaVacio((TextBox)cont))
                    {

                        return result = false;
                    }
                }
            }

            return result;
        }

    }
}
