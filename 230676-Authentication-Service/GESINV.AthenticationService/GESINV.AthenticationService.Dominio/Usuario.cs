using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Dominio
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }
        public Empresa Empresa { get; set; }
        public List<Invitacion> InvitacionesRealizadas { get; set; }
    }
}
