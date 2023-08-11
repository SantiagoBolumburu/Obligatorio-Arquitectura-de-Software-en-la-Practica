using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Dominio
{
    public class Empresa
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Integrantes { get; set; }
        public List<Invitacion> Invitaciones { get; set; }
    }
}
