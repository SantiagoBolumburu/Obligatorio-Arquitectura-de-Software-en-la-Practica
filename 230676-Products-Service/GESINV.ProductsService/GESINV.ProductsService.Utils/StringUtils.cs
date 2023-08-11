using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Utils
{
    public static class StringUtils
    {
        public static bool FormatoEmailEsValido(string email)
        {
            bool todoOk = true;
            if (string.IsNullOrEmpty(email))
            {
                todoOk = false;
            }
            else
            {
                string[] cortadoEnArroba = email.Split('@');
                if (cortadoEnArroba.Length != 2)
                    todoOk = false;
                else if (cortadoEnArroba[0].Length == 0 || cortadoEnArroba[1].Length == 0)
                    todoOk = false;
                else
                {
                    //NOMBRE
                    if (cortadoEnArroba[0].Contains("."))
                    {
                        string[] cortadoEnPunto = cortadoEnArroba[0].Split('.');
                        if (cortadoEnPunto.Length < 1)
                            todoOk = false;
                        else if (cortadoEnPunto.Any(c => c.Length == 0))
                            todoOk = false;
                    }

                    //DOMINIO
                    if (cortadoEnArroba[1].Contains("."))
                    {
                        string[] dominioCortadoEnPunto = cortadoEnArroba[1].Split('.');
                        if (dominioCortadoEnPunto.Length < 2)
                            todoOk = false;
                        else if (dominioCortadoEnPunto.Any(c => c.Length == 0))
                            todoOk = false;
                    }

                }
            }
            return todoOk;
        }

        public static string ObtenerNombreDeEmail(string email)
        {
            string nombre = email.Split('@')[0];
            return nombre;
        }
    }
}
