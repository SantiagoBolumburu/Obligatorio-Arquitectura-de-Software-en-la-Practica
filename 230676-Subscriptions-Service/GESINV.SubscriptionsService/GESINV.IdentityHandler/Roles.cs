using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.IdentityHandler
{
    public static class Roles
    {
        public const string RolAdministrador = "administrador";
        public const string RolEmpleado = "empleado";
        public const string RolAplicacion = "aplicacion";

        public static bool EsValidoParaUsuario(string rol)
        {
            bool esValido = true;

            if (!RolAdministrador.Equals(rol) && !RolEmpleado.Equals(rol))
                esValido = false;

            return esValido;
        }
    }
}
