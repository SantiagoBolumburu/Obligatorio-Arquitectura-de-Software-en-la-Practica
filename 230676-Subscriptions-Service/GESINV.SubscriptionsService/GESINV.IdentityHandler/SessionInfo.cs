using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.IdentityHandler
{
    public class SessionInfo
    {
        public static string PROPERTY_NOMBRE = "Nombre";
        public static string PROPERTY_ROL = "Rol";
        public static string PROPERTY_EMAIL = "Email";
        public static string PROPERTY_EMPRESANOMBRE = "Empresa";
        public static string PROPERTY_TIEMPOCREACION = "TiempoCreacion";
        public static string PROPERTY_SESSIONID = "SessionId";
        public static string PROPERTY_EMPRESAID = "EmpresaId";
        public static string PROPERTY_USUARIOID = "UsuarioId";

        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public DateTime TiempoCreacion { get; set; }
        public Guid SessionId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
