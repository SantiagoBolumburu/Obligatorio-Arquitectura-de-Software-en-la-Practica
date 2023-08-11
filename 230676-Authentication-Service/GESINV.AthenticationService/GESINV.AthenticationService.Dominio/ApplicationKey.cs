using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Dominio
{
    public class ApplicationKey
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ApplicationKeyStr { get; set; }
    }
}
