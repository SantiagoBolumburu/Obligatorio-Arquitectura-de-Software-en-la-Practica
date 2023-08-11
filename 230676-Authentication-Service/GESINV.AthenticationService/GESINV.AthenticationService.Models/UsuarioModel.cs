using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Models
{
    public class UsuarioModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string NombreEmpresa { get; set; }

        public void ValidateAdmin()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentNullException("El parametro 'Nombre' es obligatorio.");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException("El parametro 'Email' es obligatorio.");
            if (string.IsNullOrEmpty(Contrasenia))
                throw new ArgumentNullException("El parametro 'Contraseña' es obligatorio.");
            if (string.IsNullOrEmpty(NombreEmpresa))
                throw new ArgumentNullException("El parametro 'Nombre Empresa' es obligatorio.");
        }

        public void ValidateByInvitacion()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentNullException("El parametro 'Nombre' es obligatorio.");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException("El parametro 'Email' es obligatorio.");
            if (string.IsNullOrEmpty(Contrasenia))
                throw new ArgumentNullException("El parametro 'Contraseña' es obligatorio.");
        }
    }
}
