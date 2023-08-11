using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.Email.Service.Domain.Utils
{
    public static class StringUtils
    {
        public static string ObtenerNombreDeEmail(string email)
        {
            string nombre = email.Split('@')[0];
            return nombre;
        }
    }
}
