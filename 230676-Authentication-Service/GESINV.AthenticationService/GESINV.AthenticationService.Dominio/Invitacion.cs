using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Dominio
{
    public class Invitacion
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public bool Utilizada { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Empresa Empresa { get; set; }
        public Guid EmpresaId { get; set; }
        public Usuario Invitador { get; set; }
        public Guid InvitadorId { get; set; }
    }
}
